using System;
using Ett.Vdt.NativeApplication.Data.Pois.Payloads;

namespace Ett.Vdt.NativeApplication.Fake.Helpers
{
    internal static class PoiListFilters
    {
        public static bool Predicate(PoiPayload poi, PoiListQueryPayload query)
        {
            return IdsFilter(poi.id, query.ids);
        }

        //private static bool CategoriesIdFilter(FakeDatabase.PoiComplexQueryItem item, int[] categoriesId)
        //{
        //    if (categoriesId == null || categoriesId.Length == 0)
        //        return true;

        //    if (item.PoiCategories == null || item.PoiCategories.Length == 0)
        //        return false;

        //    foreach (var expectedId in categoriesId)
        //    {
        //        foreach (var category in item.PoiCategories)
        //        {
        //            if (category.Id == expectedId)
        //                return true;
        //        }
        //    }

        //    return false;
        //}

        //private static bool CategoriesTagFilter(FakeDatabase.PoiComplexQueryItem item, string[] categoriesTag)
        //{
        //    if (categoriesTag == null || categoriesTag.Length == 0)
        //        return true;

        //    if (item.PoiCategories == null || item.PoiCategories.Length == 0)
        //        return false;

        //    foreach (var expectedTag in categoriesTag)
        //    {
        //        foreach (var category in item.PoiCategories)
        //        {
        //            if (!string.IsNullOrEmpty(category.Tag) &&
        //                category.Tag.Equals(expectedTag, StringComparison.InvariantCultureIgnoreCase))
        //                return true;
        //        }
        //    }

        //    return false;
        //}

        private static bool IdsFilter(int poiId, int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return true;

            return Array.IndexOf(ids, poiId) >= 0;
        }

        //private static bool IgnoreCategoriesIdFilter(FakeDatabase.PoiComplexQueryItem item, int[] categoriesId)
        //{
        //    if (categoriesId == null || categoriesId.Length == 0)
        //        return true;

        //    if (item.PoiCategories == null || item.PoiCategories.Length == 0)
        //        return true;

        //    foreach (var expectedId in categoriesId)
        //    {
        //        foreach (var category in item.PoiCategories)
        //        {
        //            if (expectedId == category.Id)
        //                return false;
        //        }
        //    }

        //    return true;
        //}

        //private static bool IgnoreCategoriesTagFilter(FakeDatabase.PoiComplexQueryItem item, string[] categoriesTag)
        //{
        //    if (categoriesTag == null || categoriesTag.Length == 0)
        //        return true;

        //    if (item.PoiCategories == null || item.PoiCategories.Length == 0)
        //        return true;

        //    foreach (var expectedTag in categoriesTag)
        //    {
        //        foreach (var category in item.PoiCategories)
        //        {
        //            if (!string.IsNullOrEmpty(category.Tag) &&
        //                category.Tag.Equals(expectedTag, StringComparison.InvariantCultureIgnoreCase))
        //                return false;
        //        }
        //    }

        //    return true;
        //}
    }
}