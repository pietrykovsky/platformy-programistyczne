using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class ProblemTests
    {
        private const int SEED = 1;
        private const int ITEMS_COUNT = 10;

        private readonly List<Item> EXPECTED_ITEMS = new List<Item>
        {
            new Item(1, 3, 2),
            new Item(2, 5, 8),
            new Item(3, 7, 5),
            new Item(4, 4, 10),
            new Item(5, 2, 7),
            new Item(6, 1, 3),
            new Item(7, 4, 10),
            new Item(8, 7, 7),
            new Item(9, 3, 7),
            new Item(10, 8, 8)
        };

        private const int CAPACITY = 20;

        private readonly List<Item> EXPECTED_RESULT = new List<Item>
        {
            new Item(3, 7, 5),
            new Item(8, 7, 7),
            new Item(10, 8, 8)
        };

        private void _assertItemIsEqual(Item expected, Item actual)
        {
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Value, actual.Value);
            Assert.Equal(expected.Weight, actual.Weight);
        }

        private void _assertItemsAreEqual(List<Item> expected, List<Item> actual)
        {
            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                _assertItemIsEqual(expected[i], actual[i]);
            }
        }

        [Fact]
        public void Solve_NoCapacity_ReturnsEmptyList()
        {
            var problem = new Problem(ITEMS_COUNT, SEED);
            var result = problem.Solve(0);
            Assert.Empty(result.Items);
        }

        [Fact]
        public void Solve_MeetsLimitations_ReturnsAtleastOneItem()
        {
            var problem = new Problem(ITEMS_COUNT, SEED);
            var result = problem.Solve(CAPACITY);

            var containsAtleastOneItem = result.Items.Count > 0;

            Assert.True(containsAtleastOneItem, "Result should contain atleast one item");
        }

        [Fact]
        public void Solve_SpecificCapacity_ReturnsExpectedItems()
        {
            var problem = new Problem(ITEMS_COUNT, SEED);
            var result = problem.Solve(CAPACITY);
            _assertItemsAreEqual(EXPECTED_RESULT, result.Items);
        }

        [Fact]
        public void Solve_DifferentItemsOrder_ReturnsExpectedItems()
        {
            var problem = new Problem(ITEMS_COUNT, SEED);
            problem.Items.Reverse();
            var result = problem.Solve(CAPACITY);

            Assert.Equal(EXPECTED_RESULT.Count, result.Items.Count);
            _assertItemsAreEqual(EXPECTED_RESULT, result.Items);
        }

        [Fact]
        public void Problem_WhenCreated_ItemsAreGenerated()
        {
            var problem = new Problem(ITEMS_COUNT, SEED);
            var actualItems = problem.Items;
            
            Assert.Equal(EXPECTED_ITEMS.Count, actualItems.Count);
            _assertItemsAreEqual(EXPECTED_ITEMS, actualItems);
        }

        [Fact]
        public void Problem_ZeroItemCountWhenCreated_ReturnsEmptyList()
        {
            var problem = new Problem(0, SEED);
            var actualItems = problem.Items;
            Assert.Empty(actualItems);
        }

        [Fact]
        public void BaseItemsContainer_ItemsToListOfStrings_ReturnsExpectedString()
        {
            var container = new BaseItemsContainer();
            container.Items = new List<Item>
            {
                new Item(1, 1, 1),
                new Item(2, 2, 2)
            };
            var expected = new List<string>
            {
                "Id: 1, Value: 1, Weight: 1",
                "Id: 2, Value: 2, Weight: 2"
            };

            var items = container.ItemsToListOfStrings();

            Assert.Equal(expected.Count, items.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], items[i]);
            }
        }
        
        [Fact]
        public void Item_ToString_ReturnsExpectedString()
        {
            var item = new Item(1, 1, 1);
            var expected = "Id: 1, Value: 1, Weight: 1";
            Assert.Equal(expected, item.ToString());
        }
    }
}
