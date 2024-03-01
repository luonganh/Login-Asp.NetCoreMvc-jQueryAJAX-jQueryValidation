namespace Asp.NetCore.Shared.Helpers
{
    public static class ObjectHelper
    {
        public static TU CloneToModel<T, TU>(this T source) where TU : new()
        {
            if (source == null) return default(TU);

            var dest = new TU();
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    {
                        // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }
            return dest;
        }

        public static List<TU> CloneToListModels<T, TU>(this List<T> source) where TU : new()
        {
            if (source == null) return null;

            var dest = new List<TU>();

            foreach (var item in source)
            {
                var destItem = item.CloneToModel<T, TU>();
                dest.Add(destItem);
            }//foreach

            return dest;
        }
    }
}
