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

    public interface IPing
    {
        Tuple<string, string> StringTuple { get; set; }
    }

    public class PingA : IRequest, IPing
    {
        public Tuple<string, string> StringTuple { get; set; }
        public PingA()
        {

        }
    }

    public class PingAHandler : RequestHandler<PingA>
    {
        protected override void Handle(PingA request)
        { 
           Console.WriteLine($"PING A, item 1: {request.StringTuple.Item1} : {request.StringTuple.Item2}");
        }
    }

    public class PingB : IRequest, IPing
    {
        public Tuple<string, string> StringTuple { get; set; }
        public PingB()
        {
        }
    }

    public class PingBHandler : RequestHandler<PingB>
    {
        protected override void Handle(PingB request)
        {
            Console.WriteLine($"PING B, item 1: {request.StringTuple.Item1} : {request.StringTuple.Item2}");
        }
    }

    public class PingC : IRequest, IPing
    {
        public Tuple<string, string> StringTuple { get; set; }
        public PingC()
        {
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
