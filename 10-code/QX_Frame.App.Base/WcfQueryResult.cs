using System.Collections;

namespace QX_Frame.App.Base
{
    public class WcfQueryResult
    {
        public WcfQueryResult(object data)
        {
            this.Data = data;
        }
        public object Data { get; set; }
        public int TotalCount { get; set; }
        public TResult Cast<TResult>() where TResult : class
        {
            return (TResult)this.Data;
        }
        public TResult Cast<TResult>(out int totalCount) where TResult : class
        {
            totalCount = this.TotalCount;
            return (TResult)this.Data;
        }
        public void Fill(IList list)
        {
            IEnumerable data = this.Data as IEnumerable;
            foreach (object obj2 in data)
            {
                list.Add(obj2);
            }
        }
        public void Fill(IList list, out int totalCount)
        {
            IEnumerable data = this.Data as IEnumerable;
            foreach (object obj2 in data)
            {
                list.Add(obj2);
            }
            totalCount = this.TotalCount;
        }
    }
}
