using FirstProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThirtyOne.Helpers
{
    public static class CardListExtensions
    {

        /// <summary>
        /// Calculate score for a list of cards
        /// </summary>
        /// <param name="Cards"></param>
        /// <returns></returns>
        public static int CalculateScore(this IEnumerable<Card> Cards)
        {
            return Cards
                .GroupBy(card => card.suit)
                .OrderByDescending(grp => grp.Sum(card => card.value))
                .First()
                .Sum(card => card.value);
        }

        /// <summary>
        /// Create a string list of all cards
        /// </summary>
        /// <param name="Cards"></param>
        /// <returns></returns>
        public static string ToListString(this IEnumerable<Card> Cards)
        {
            return string.Join(",", Cards);
        }
    }
}
