using System.Numerics;

namespace Laba_2_prob;

public class Calculator
{
    public string display { get; private set; } = "0";
    public string calculate { get; private set; } = "";
    public bool is_new { get; private set; } = false;
    public bool is_error { get; private set; } = false;

   
    public void Set_display(string value) 
    {
        if (value.Length > 20) return; // Заборона введення більше 20 символів
        display = value.Length > 15 ? value.Substring(0, 15) : value;
    }

    public void Set_calculate(string value) 
    {
        if (value.Length > 25) value = value.Substring(0, 25); // Обрізання довжини
        calculate = value;
    }

    public void Set_is_new(bool value) => is_new = value;

    public void restart_error()
    {
        if (is_error)
        {
            display = "0";
            calculate = "";
            is_new = true;
            is_error = false;
        }
    }

    public void Set_is_error()
    {
        display = "Error";
        calculate = "";
        is_new = true;
        is_error = true;
    }

    public void add_decimal()
    {
        if (is_new)
        {
            display = "0,";
            is_new = false;
        }
        else if (!display.Contains(","))
        {
            display += ",";
        }
    }

    public void back()
    {
        if (display == "0" || (display.Length != 1 && display[0] == '-'))
        {
            display = "0";
            is_new = true;
        }
        else
        {
            display = display.Substring(0, display.Length - 1);
        }
    }

    public void clear()
    {
        display = "0";
        calculate = "";
        is_new = true;
    }

    public void input_number(string number)
    {
        if (display.Length >= 20) return; // Якщо вже 20 символів, заборонити подальший ввід

        if (is_new)
        {
            display = number;
            is_new = false;
        }
        else
        {
            if (display == "0" && number != ",")
                display = number;
            else
                display += number;
        }
    }


    public void input_operation(string operation)
    {
        if (operation == "(")
        {
            if (calculate.EndsWith("=")) 
            {
                calculate = "(";
            }
            else
            {
                calculate += " (";
            }
            display = ""; 
            is_new = false;
        }
        else if (operation == ")") 
        {
            if (!string.IsNullOrEmpty(display) && display != "0")
            {
                calculate += " " + display;
            }
            calculate += " )"; 
            display = "";
            is_new = false;
        }

        else 
        {
            if (calculate.EndsWith("=")) 
            {
                calculate = display + " " + operation;
            }
            else
            {
                calculate += " " + display + " " + operation;
            }
            display = "0"; 
            is_new = true;
        }
    }



    public void calculate_expression(out string old_display, out string old_calculate)
    {
        old_display = display;
        old_calculate = calculate;

        if (display.EndsWith(",")) 
            display += "0";

        if (!string.IsNullOrWhiteSpace(display) && !calculate.EndsWith(display))
            calculate += " " + display;

        try
        {
            Console.WriteLine(calculate);
            string rpn = ConvertToRPN(calculate); 
            double result = EvaluateRPN(rpn);

            calculate += " =";
            display = result.ToString();

            // Обрізка результату до 25 символів
            if (display.Length > 25)
                display = display.Substring(0, 25);

            is_new = true;
        }
        catch (Exception e)
        {
            Set_is_error();
            Console.WriteLine("Помилка обчислення: " + e.Message);
        }
    }


    private string ConvertToRPN(string expression)
    {
        Dictionary<string, int> precedence = new()
        {
            ["+"] = 1, ["-"] = 1,
            ["*"] = 2, ["/"] = 2, ["mod"] = 2,
            ["^"] = 3
        };

        Stack<string> operators = new();
        List<string> output = new();
        string[] tokens = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                output.Add(token);
            }
            else if (precedence.ContainsKey(token))
            {
                while (operators.Count > 0 && precedence.ContainsKey(operators.Peek()) &&
                       precedence[operators.Peek()] >= precedence[token])
                {
                    output.Add(operators.Pop());
                }
                operators.Push(token);
            }
            else if (token == "(")
            {
                operators.Push(token);
            }
            else if (token == ")")
            {
                while (operators.Count > 0 && operators.Peek() != "(")
                {
                    output.Add(operators.Pop());
                }
                if (operators.Count == 0)
                {
                    throw new ArgumentException("Неправильне розміщення дужок");
                }
                operators.Pop(); 
            }
            else
            {
                throw new ArgumentException($"Невідомий: {token}");
            }
        }

        while (operators.Count > 0)
        {
            string op = operators.Pop();
            if (op == "(")
            {
                throw new ArgumentException("Неправильне розміщення дужок");
            }
            output.Add(op);
        }

        return string.Join(" ", output);
    }


private double EvaluateRPN(string rpn)
{
    Dictionary<string, Func<double, double, double>> operations = new()
    {
        ["+"] = (a, b) => a + b,
        ["-"] = (a, b) => a - b,
        ["*"] = (a, b) => a * b,
        ["/"] = (a, b) => b == 0 ? throw new DivideByZeroException("Division by zero") : a / b,
        ["mod"] = (a, b) => b == 0 ? throw new DivideByZeroException("Modulo by zero") : a % b,
        ["^"] = (a, b) => Math.Pow(a, b)
    };

    Stack<double> values = new();
    string[] tokens = rpn.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    foreach (string token in tokens)
    {
        if (double.TryParse(token, out double num))
        {
            values.Push(num);
        }
        else if (operations.ContainsKey(token))
        {
            if (values.Count < 2) throw new ArgumentException("Invalid expression");

            double b = values.Pop();
            double a = values.Pop();
            values.Push(operations[token](a, b));
        }
        else
        {
            throw new ArgumentException($"Unknown operator: {token}");
        }
    }

    if (values.Count != 1) throw new ArgumentException("Invalid expression");

    return values.Pop();
}


    public void unary_expression(string operation,out string old_display, out string old_calculate)
    {
        old_display = display;
        old_calculate = calculate;

        double input = 0;


            if (!double.TryParse(display, out input))
            {
                throw new ArgumentException("Bad input!");
            }
        
        double result = 0;
        string new_calculate = "";

        var operations = new Dictionary<string, Func<double, (double, string)>>
        {
            ["1/x"] = _ => (1/input, $"1/{input}"),
            ["exp"]=input=>(Math.Pow(Math.E,input),$"exp({input})"),
            ["\u00b1"] = _ => (-input, $"negate({input})"),
            ["√"] = input =>
                input < 0
                    ? throw new ArgumentException("sqrt only for positive number")
                    : (Math.Sqrt(input), $"√({input})"),
            ["x²"] = input => (Math.Pow(input, 2), $"({input})²"),
            ["|x|"]=input=>(Math.Abs(input),$"|{input}|"),
            ["10ˣ"] = input => (Math.Pow(10, input), $"10^{input}"),
            ["x\u00b3"]=input=>(Math.Pow(input,3),$"{input}³"),
            ["\u221bx"]=input=>(Math.Pow(input,1/3.0),$"∛{input}"),
            ["x!"]= input =>(Factorial(input), $"{input}!"),
            ["log"] = input =>
                input <= 0
                    ? throw new ArgumentException("log on non-positive")
                    : (Math.Log10(input), $"log({input})"),
            ["ln"] = input =>
                input <= 0
                    ? throw new ArgumentException("log on non-positive")
                    : (Math.Log(input), $"ln({input})")
        };

        if (operations.TryGetValue(operation, out var func))
        {
            (result, new_calculate) = func(input);
        }
        else
        {
            throw new ArgumentException("Unknown operation");
        }

        display = result.ToString();
        if (operation == "π" || operation == "e")
        {
            calculate = result.ToString()+"=";
        }
        else
        {
            calculate = new_calculate + " =";
        }
        is_new = true;
    }
    static double Factorial(double a)
    {
        int n = (int)a;
        if (n < 0) throw new ArgumentException("Факторіал визначений тільки для невід’ємних чисел");

        int result = 1;
        for (int i = 2; i <= n; i++)
            result *= i;

        return (double)result;
    }

}
public interface ICommand {
    void Execute();
    void Undo();
}

public class Manager
{
    private Stack<ICommand> undoStack = new Stack<ICommand>();

    public void Execute(ICommand command)
    {
        command.Execute();
        undoStack.Push(command);
    }

    public void Undo()
    {
        if (undoStack.Count > 0)
        {
            ICommand command = undoStack.Pop();
            command.Undo();
        }
    }
}
public abstract class Calculator_Commands(Calculator calculator) : ICommand {
    protected readonly Calculator calculator = calculator;
    protected string old_display = calculator.display;
    protected string old_calculate = calculator.calculate;
    protected bool old_is_new = calculator.is_new;

    public abstract void Execute();

    public virtual void Undo() {
        calculator.Set_display(old_display);
        calculator.Set_calculate(old_calculate);
        calculator.Set_is_new(old_is_new);
    }
}

public class Input_Command : Calculator_Commands
{
    private Calculator calculator;
    private string number;

    public Input_Command(Calculator calculator, string number) : base(calculator) 
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        this.number = number;
    }

    public override void Execute()
    {
        calculator.input_number(number);
    }
}
public class Decimal_Command : Calculator_Commands
{
    private Calculator calculator;
    public Decimal_Command(Calculator calculator) : base(calculator) 
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        
    }

    public override void Execute()
    {
        calculator.add_decimal();
    }
}
public class Back_Command : Calculator_Commands
{
    private Calculator calculator;
    public Back_Command(Calculator calculator) : base(calculator) 
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        
    }

    public override void Execute()
    {
        calculator.back();
    }
}
public class Operator_Command : Calculator_Commands
{
    private Calculator calculator;
    private string opera;

    public Operator_Command(Calculator calculator, string opera) : base(calculator) 
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        this.opera = opera;
    }

    public override void Execute()
    {
        calculator.input_operation(opera);
    }
}
public class Calculate_Command : Calculator_Commands
{
    public Calculate_Command(Calculator calculator) : base(calculator)
    {
    }

    public override void Execute()
    {
        calculator.calculate_expression(out old_display, out old_calculate);
    }
}

public class Unary_Command : Calculator_Commands
{
    private Calculator calculator;
    private string opera;

    public Unary_Command(Calculator calculator, string opera) : base(calculator)
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        this.opera = opera;
    }

    public override void Execute()
    {
        calculator.unary_expression(opera, out _,out _);
    }
}
public class Clear_Command : Calculator_Commands
{
    private Calculator calculator;

    public Clear_Command(Calculator calculator) : base(calculator)
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        
    }

    public override void Execute()
    {
        calculator.clear();
        
    }
}