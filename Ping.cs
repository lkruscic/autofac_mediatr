using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace autofac_mediatR
{

    public interface IPing : IRequest
    {
        string Message { get; set; }
    }

    public class PingA : IPing
    {
        public string Message { get; set; }
        public PingA()
        {

        }
    }

    public class PingAHandler : RequestHandler<PingA>
    {
        protected override void Handle(PingA request)
        {
            Console.WriteLine($"PING A request : {request.Message} handled by PingAHanlder! ");
        }
    }

    public class PingB : IPing
    {
       public string Message { get; set; }
        public PingB()
        {
        }
    }

    public class PingBHandler : RequestHandler<PingB>
    {
        protected override void Handle(PingB request)
        {
           Console.WriteLine($"PING B request : {request.Message} handled by PingBHanlder! ");
        }
    }

    public class PingC : IPing
    {
       public string Message { get; set; }
        public PingC()
        {
        }
    }

    public class PingCHandler : RequestHandler<PingC>
    {
        protected override void Handle(PingC request)
        {
           Console.WriteLine($"PING C request : {request.Message} handled by PingCHanlder! ");
        }
    }
}
