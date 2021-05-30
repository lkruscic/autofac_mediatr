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


    public class PingA : IRequest
    {
        public Tuple<string, string> StringTuple;
        public PingA()
        {

        }

        //public PingA(Tuple<string, string> t)
        //{
        //    StringTuple = t;
        //}
    }

    public class PingAHandler : RequestHandler<PingA>
    {
        protected override void Handle(PingA request)
        {
            Console.WriteLine($"PING A, item 1: {request.StringTuple.Item1} : {request.StringTuple.Item2}");
        }
    }

    public class PingB : IRequest
    {
        public Tuple<string, string> StringTuple;
        public PingB(Tuple<string, string> t)
        {
            StringTuple = t;
        }
    }

    public class PingBHandler : RequestHandler<PingB>
    {
        protected override void Handle(PingB request)
        {
            Console.WriteLine($"PING B, item 1: {request.StringTuple.Item1} : {request.StringTuple.Item2}");
        }
    }

    public class PingC : IRequest
    {
        public Tuple<string, string> StringTuple;
        public PingC(Tuple<string, string> t)
        {
            StringTuple = t;
        }
    }

    public class PingCHandler : RequestHandler<PingC>
    {
        protected override void Handle(PingC request)
        {
            Console.WriteLine($"PING C, item 1: {request.StringTuple.Item1} : {request.StringTuple.Item2}");
        }
    }
}
