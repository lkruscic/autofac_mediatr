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
    public interface IPongMessage
    {
        object Message { get; set; }
    }

    public class PongAMessage : IPongMessage
    {
        public object Message { get; set; }
    }
    
    public class PongBMessage : IPongMessage
    {
        public object Message { get; set; }
    }

    public class PongCMessage : IPongMessage
    {
        public object Message { get; set; }
    }


    public interface IPong : IRequest<IPongMessage>
    {
        string Message { get; set; }
    }

    #region Pong A
    public class PongA : IPong
    {
        public string Message { get; set; }
        public PongA()
        {

        }
    }

    public class PongAHandler : RequestHandler<PongA, IPongMessage>
    {
        protected override IPongMessage Handle(PongA request)
        {
            var msg = new PongAMessage { Message = $"Message: {request.Message}, I'm a new Pong A return class!" };
            Console.WriteLine(msg.Message);
            return msg;
        }
    }
    #endregion

    #region Pong B
    public class PongB : IPong
    {
        public string Message { get; set; }
        public PongB()
        {

        }
    }

    public class PongBHandler : RequestHandler<PongB, IPongMessage>
    {
        protected override IPongMessage Handle(PongB request)
        {
            var msg = new PongBMessage { Message = $"Message: {request.Message}, I'm a new Pong B return class!" };
            Console.WriteLine(msg.Message);
            return msg;
        }
    }
    #endregion
    #region Pong C
    public class PongC : IPong
    {
        public string Message { get; set; }
        public PongC()
        {

        }
    }

    public class PongCHandler : RequestHandler<PongC, IPongMessage>
    {
        protected override IPongMessage Handle(PongC request)
        {
            var msg = new PongCMessage { Message = $"Message: {request.Message}, I'm a new Pong C return class!" };
            Console.WriteLine(msg.Message);
            return msg;
        }
    }
    #endregion



}