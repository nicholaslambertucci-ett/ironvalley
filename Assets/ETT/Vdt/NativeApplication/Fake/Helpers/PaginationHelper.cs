using System;

namespace Ett.Vdt.NativeApplication.Fake.Helpers
{
    public static class PaginationHelper
    {
        public static T[] Paginate<T>(T[] items, int limit, int offset)
        {
            if (items == null || items.Length == 0)
                return new T[0];

            offset = offset > -1
                ? offset >= items.Length ? items.Length - 1 : offset
                : 0;

            limit = limit > -1
                ? (limit + offset) > items.Length ? items.Length - offset : limit
                : items.Length - offset;


            var result = new T[limit];
            Array.Copy(items, offset, result, 0, limit);
            return result;
        }
    }
}