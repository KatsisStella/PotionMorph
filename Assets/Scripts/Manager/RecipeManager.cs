using NUnit.Framework;
using PotionMorph.Persistency;
using PotionMorph.SO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PotionMorph.Manager
{
    public class RecipeManager : MonoBehaviour
    {
        public static RecipeManager Instance { private set; get; }

        [SerializeField]
        private TMP_Text _recipeText, _recipeEffect;

        [SerializeField]
        private RecipeInfo[] _recipes;

        [SerializeField]
        private TMP_Text _importantRecipes;

        [SerializeField]
        private Transform _ingredientContainer;

        [SerializeField]
        private GameObject _ingredientPrefab;

        [SerializeField]
        private GameObject _cum, _femaleJuice, _milk, _pheromones, _pubes, _saliva, _sweat, _urine;

        private IngredientInfo _lastIngredient;

        private List<AethraIngredient> _unlockedIngredients = new();

        private void Awake()
        {
            Instance = this;
            UpdateRecipeDisplay();

            foreach (var ingr in PersistencyManager.Instance.SaveData.AethraIngredients)
            {
                SpawnIngredientButton(ingr);
            }
        }

        private void SpawnIngredientButton(AethraIngredient ingr)
        {
            _unlockedIngredients.Add(ingr);
            var go = ingr switch
            {
                AethraIngredient.Cum => _cum,
                AethraIngredient.FemaleJuice => _femaleJuice,
                AethraIngredient.Milk => _milk,
                AethraIngredient.Pheromones => _pheromones,
                AethraIngredient.Pubes => _pubes,
                AethraIngredient.Saliva => _saliva,
                AethraIngredient.Sweat => _sweat,
                AethraIngredient.Urine => _urine,
                _ => throw new System.NotImplementedException()
            };
            var target = Instantiate(_ingredientPrefab, _ingredientContainer);
            target.GetComponent<Image>().sprite = go.GetComponent<SpriteRenderer>().sprite;
            target.GetComponent<Button>().onClick.AddListener(() =>
            {
                SpawnIngredient(go);
            });
        }

        public void AddIngredient(AethraIngredient ingr)
        {
            if (!PersistencyManager.Instance.SaveData.AethraIngredients.Contains(ingr))
            {
                SpawnIngredientButton(ingr);
                PersistencyManager.Instance.SaveData.AethraIngredients.Add(ingr);
            }
        }

        public void SpawnIngredient(GameObject ingredient)
        {
            Instantiate(ingredient, Vector3.zero, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }

        private void UpdateRecipeDisplay()
        {
            _importantRecipes.text = string.Join("\n", PersistencyManager.Instance.SaveData.DiscoveredRecipes.OrderBy(x => x));
        }

        public void LoadRecipe(IngredientInfo[] ingredients)
        {
            _recipeText.gameObject.SetActive(true);
            _recipeEffect.gameObject.SetActive(true);

            Debug.Log($"Got recipe with {string.Join(", ", ingredients.Select(x => x.Name))}");
            var targetRecipe = _recipes.FirstOrDefault(x => x.Ingredients.Length == ingredients.Length && x.Ingredients.All(i => ingredients.Contains(i)));
            var gotAllSame = ingredients.Distinct().Count() == 1;

            if (targetRecipe != null)
            {
                if (!PersistencyManager.Instance.SaveData.DiscoveredRecipes.Contains(targetRecipe.Name))
                {
                    PersistencyManager.Instance.SaveData.DiscoveredRecipes.Add(targetRecipe.Name);
                    UpdateRecipeDisplay();
                }

                _recipeText.text = targetRecipe.Name;
                _recipeEffect.text = targetRecipe.Description;
                foreach (var effect in targetRecipe.Effect)
                {
                    switch (effect)
                    {
                        case RecipeEffect.IncreaseBreast: PersistencyManager.Instance.SaveData.UpdateBreast(true); break;
                        case RecipeEffect.DecreaseBreast: PersistencyManager.Instance.SaveData.UpdateBreast(false); break;
                        case RecipeEffect.IncreasePenis: PersistencyManager.Instance.SaveData.UpdatePenis(true); break;
                        case RecipeEffect.DecreasePenis: PersistencyManager.Instance.SaveData.UpdatePenis(false); break;
                        case RecipeEffect.IncreasePubes: PersistencyManager.Instance.SaveData.UpdateBodyHair(true); break;
                        case RecipeEffect.DecreasePubes: PersistencyManager.Instance.SaveData.UpdateBodyHair(false); break;
                        case RecipeEffect.EnablePenis: PersistencyManager.Instance.SaveData.TogglePenis(true); break;
                        case RecipeEffect.DisablePenis: PersistencyManager.Instance.SaveData.TogglePenis(false); break;
                        case RecipeEffect.EnablePheromones: PersistencyManager.Instance.SaveData.TogglePheromoneCloud(true); break;
                        case RecipeEffect.DisablePheromones: PersistencyManager.Instance.SaveData.TogglePheromoneCloud(false); break;
                        case RecipeEffect.EnableSweat: PersistencyManager.Instance.SaveData.ToggleSweat(true); break;
                        case RecipeEffect.DisableSweat: PersistencyManager.Instance.SaveData.ToggleSweat(false); break;
                        case RecipeEffect.EnablePregnancy: PersistencyManager.Instance.SaveData.TogglePregnancy(true); break;
                        case RecipeEffect.DisablePregnancy: PersistencyManager.Instance.SaveData.TogglePregnancy(false); break;
                        case RecipeEffect.UnsetJuice: PersistencyManager.Instance.SaveData.SetJuice(Juice.None); break;
                        case RecipeEffect.SetUrine: PersistencyManager.Instance.SaveData.SetJuice(Juice.Urine); break;
                        case RecipeEffect.SetFemaleJuice: PersistencyManager.Instance.SaveData.SetJuice(Juice.FemaleJuice); break;
                        case RecipeEffect.SetExpIdle: PersistencyManager.Instance.SaveData.SetExpression(Expression.Idle); break;
                        case RecipeEffect.SetExpHorny: PersistencyManager.Instance.SaveData.SetExpression(Expression.Horny); break;
                        case RecipeEffect.SetExpExcited: PersistencyManager.Instance.SaveData.SetExpression(Expression.Excited); break;
                        case RecipeEffect.SetExpSurprised: PersistencyManager.Instance.SaveData.SetExpression(Expression.Surprised); break;
                        case RecipeEffect.SetExpBlush: PersistencyManager.Instance.SaveData.SetExpression(Expression.Blush); break;
                        case RecipeEffect.SetPonytail: PersistencyManager.Instance.SaveData.SetHair(HairStyle.Ponytail); break;
                        case RecipeEffect.SetBraids: PersistencyManager.Instance.SaveData.SetHair(HairStyle.Braids); break;
                        case RecipeEffect.SetSmallHair: PersistencyManager.Instance.SaveData.SetHair(HairStyle.SmallHair); break;
                        case RecipeEffect.SetLongHair: PersistencyManager.Instance.SaveData.SetHair(HairStyle.LongHair); break;
                        default: throw new System.NotImplementedException($"Effect {targetRecipe.Effect} was not implemented");
                    }
                }

                PersistencyManager.Instance.Save();
                AethraManager.Instance.UpdateAethra();

                _lastIngredient = null;
            }
            else if (gotAllSame)
            {
                _recipeText.text = ingredients[0].ThreeName;
                if (ingredients.Length == 1)
                {
                    _recipeEffect.text = _lastIngredient == ingredients[0] ? "There must be a recipe that would fully use the effects of this" : $"This tastes {ingredients[0].Hint}";
                    _lastIngredient = ingredients.Length == 1 ? null : ingredients[0];
                }
                else
                {
                    _recipeEffect.text = $"This tastes {ingredients[0].Hint}";
                    _lastIngredient = null;
                }
            }
            else if (ingredients.Distinct().Count() == 2)
            {
                var groups = ingredients.GroupBy(x => x.Name).OrderByDescending(x => x.Count()).ToArray();
                _recipeText.text = $"{groups[0].ElementAt(0).TwoAdjective} {groups[1].ElementAt(0).SingleAdjective}";
                _recipeEffect.text = $"This recipe need to be more {GetClosestUndiscoveredIngredient(ingredients).Hint}";

                _lastIngredient = null;
            }
            else
            {
                _recipeText.text = "Unidentified mix";
                _recipeEffect.text = $"This recipe need to be more {GetClosestUndiscoveredIngredient(ingredients).Hint}";

                _lastIngredient = null;
            }


            StartCoroutine(RemoveRecipeText());
        }

        private IngredientInfo GetClosestUndiscoveredIngredient(IngredientInfo[] ingredients)
        {
            var pending = _recipes
                .Where(x => !PersistencyManager.Instance.SaveData.DiscoveredRecipes.Any(y => y == x.Name))
                .Where(x =>  x.Ingredients.All(y => y.AethraIngredient == AethraIngredient.NotApplicable || _unlockedIngredients.Contains(y.AethraIngredient)));
            if (pending.Any())
            {
                return pending
                    .OrderBy(x => x.Ingredients.Count(y => ingredients.Any(z => z.Name == y.Name)))
                    .First()
                    .Ingredients
                    .Where(x => !ingredients.Any(y => y.Name == x.Name))
                    .First();
            }
            return null;
        }

        private IEnumerator RemoveRecipeText()
        {
            yield return new WaitForSeconds(3f);
            _recipeText.gameObject.SetActive(false);
            _recipeEffect.gameObject.SetActive(false);
        }
    }
}
