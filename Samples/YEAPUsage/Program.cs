﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YEAPUsage
{
    class Todo
    {
        public string Description { get; }

        public Todo(string id ,string description)
        {
            //throw an ArgumentNullException if the value is null or whitespace
            Id = id.GuardAgainstNullOrEmpty(nameof(id))
                .GuardAgainst(v=> !v.IsGuidValue(), $"the given id : [{id}] is not a valid guid");

            Description = description.GuardAgainstNullOrEmpty(nameof(description));
            
        }

        public string Id { get; }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var s = "some value";

            //s.IsNullOrEmpty();
            //s.EqualsIgnoreCaseCurrent() //CurrentCulture
            //s.EqualsIgnoreCaseInvariant() 
            //s.EqualsIgnoreCaseOrdinal()
            //s.Concat(new []{"1","2"})

            var list = new List<Todo>
            {
                new Todo(Guid.NewGuid().ToString(),"Todo1"),
                new Todo(Guid.NewGuid().ToString(),"Todo2"),
                new Todo(Guid.NewGuid().ToString(),"Todo3")
            };


            // list.IsNullOrEmpty();
            //list.IsNotEmpty()
            // list.AsReadOnly();
            //list.EmptyIfNull()

            var todo = new Todo(Guid.NewGuid().ToString(), "Todo4");

            //todo.IsNotNull();
            //todo.IsNull();
            //todo.DefaultIfNull(new Todo(Guid.Empty.ToString(), "--"));

            //todo.AsTaskFromResult() => returns Task<Todo>

            ExampleOfNext.HandleState(new State(10));//value will be 2
            

            Console.WriteLine("Hello World!");
        }
    }

    public static class ExampleOfNext
    {
        public static void HandleState(State state)
        {
            var newstate = state
                        .Next(SetToZero)
                        .Next(SetToOne)
                        .Next(SetToToo);

            Console.WriteLine($"State Value {newstate.Value}");
        }

        private static State SetToOne(State state)
        {
            if (state.Value == 1)
                return state;

            return state.Change(1);
        }

        private static State SetToToo(State state)
        {
            if (state.Value == 2)
                return state;

            return state.Change(2);
        }

        private static State SetToZero(State state)
        {
            if (state.Value == 0)
                return state;

            return state.Change(0);
        }


    }

    public class State
    {
        public int Value { get; }

        public State(int value)
        {
            Value = value;
        }

        public State Change(int value)
            => new State(value);
    }
}
