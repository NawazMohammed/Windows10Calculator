namespace Calculator.Models.Commands
{
    public interface INumericCommand
    {
         char Name { get; }

         int Value { get; }
    }

    public class Zero : INumericCommand
    {
        public char Name => '0';
        public int Value => 0;
    }

    public class One : INumericCommand
    {
        public char Name => '1';
        public int Value => 1;
    }

    public class Two : INumericCommand
    {
        public char Name => '2';
        public int Value => 2;
    }

    public class Three : INumericCommand
    {
        public char Name => '3';
        public int Value => 3;
    }

    public class Four : INumericCommand
    {
        public char Name => '4';
        public int Value => 4;
    }

    public class Five : INumericCommand
    {
        public char Name => '5';
        public int Value => 5;
    }

    public class Six : INumericCommand
    {
        public char Name => '6';
        public int Value => 6;
    }

    public class Seven : INumericCommand
    {
        public char Name => '7';
        public int Value => 7;
    }

    public class Eight : INumericCommand
    {
        public char Name => '8';
        public int Value => 8;
    }

    public class Nine : INumericCommand
    {
        public char Name => '9';
        public int Value => 9;
    }

    public class Point : INumericCommand
    {
        public char Name => '.';
        public int Value => 0;
    }

    public class A : INumericCommand
    {
        public char Name => 'A';
        public int Value => 10;
    }

    public class B : INumericCommand
    {
        public char Name => 'B';
        public int Value => 11;
    }

    public class C : INumericCommand
    {
        public char Name => 'C';
        public int Value => 12;
    }

    public class D : INumericCommand
    {
        public char Name => 'D';
        public int Value => 13;
    }

    public class E : INumericCommand
    {
        public char Name => 'E';
        public int Value => 14;
    }

    public class F : INumericCommand
    {
        public char Name => 'F';
        public int Value => 15;
    }


}
