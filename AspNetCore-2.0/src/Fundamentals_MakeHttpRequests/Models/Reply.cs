using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentals_MakeHttpRequests.Models
{
    public interface IHelloClient
    {
        [Get("/helloworld")]
        Task<Reply> GetMessageAsync();
    }

    public class Reply
    {
        public string Message { get; set; }
    }
}
