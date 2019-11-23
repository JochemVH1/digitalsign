using System.Collections.Generic;
using digitalsign.common.Enumeration;

namespace digitalsign.common.ApiResult
{
    public class ApiResult<T> {
        public bool Success { get; set; }
        public T ContainingObject { get; set; }
        public StatusCode StatusCode { get; set; }
        public IList<string> Notifications { get; set; }

        public ApiResult() {
            Notifications = new List<string>();
        }
    }
}
