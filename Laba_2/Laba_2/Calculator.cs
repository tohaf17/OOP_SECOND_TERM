namespace Laba_2;
public class Calculator
{
    public string display { get; private set; } = "0";
    public string calculate { get; private set; } = "";
    public bool is_new { get; private set; } = false;
    public bool is_error { get; private set; } = false;

    public void Set_display(string value) => display = display.Length>15?display.Substring(0, 15):value;
    public void Set_calculate(string value) => calculate = display.Length>15?display.Substring(0, 15):value;
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
        if (is_new)
        {
            display = number;
            is_new = false;
        }
        else
        {
            if (display == "0" && number != ",")
            {
                display = number;
            }
            else
            {
                display += number;
            }
        }
    }

    public void input_operation(string operation)
    {
        if (display.EndsWith(",")) 
            display += "0"; 
    
        calculate = display + " " + operation;
        is_new = true;
    }


    public void extra_operation(string operation, out string old_display, out string old_calculate)
    {
        old_display = display;
        old_calculate = calculate;

        double input = 0;

        if (operation != "π" && operation != "e") 
        {
            if (!double.TryParse(display, out input))
            {
                throw new ArgumentException("Bad input!");
            }
        }

        double result = 0;
        string new_calculate = "";

        var operations = new Dictionary<string, Func<double, (double, string)>>
        {
            ["π"] = _ => (Math.PI, "π"),
            ["e"] = _ => (Math.E, "e"),
            ["√"] = input =>
                input < 0
                    ? throw new ArgumentException("sqrt only for positive number")
                    : (Math.Sqrt(input), $"√({input})"),
            ["x²"] = input => (Math.Pow(input, 2), $"({input})²"),
            ["ln"] = input =>
                input <= 0
                    ? throw new ArgumentException("ln on non-positive")
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
        calculate = new_calculate;
        is_new = true;
    }


    public void calculate_expression(out string old_display, out string old_calculate, out double first,
        out double second, out string operation)
    {
        old_display = display;
        old_calculate = calculate;
    
        // 🔹 Автоматично доповнюємо "3," → "3,0"
        if (display.EndsWith(","))
            display += "0";

        string[] operands = calculate.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (!double.TryParse(operands[0], out first))
            throw new ArgumentException("error");

        operation = operands[1];

        if (!double.TryParse(display, out second))
            throw new ArgumentException("error");

        double result = 0;
        var operations = new Dictionary<string, Func<double, double, double>>
        {
            ["+"] = (first, second) => first + second,
            ["-"] = (first, second) => first - second,
            ["*"] = (first, second) => first * second,
            ["/"] = (first, second) => second == 0 ? throw new DivideByZeroException("error") : first / second
        };

        if (operations.TryGetValue(operation, out var func))
        {
            result = func(first, second);
        }

        calculate = calculate + " " + display + " =";
        display = result.ToString();
        is_new = true;
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

    public Input_Command(Calculator calculator, string number) : base(calculator) // Якщо у батьківському класі є конструктор, який приймає Calculator
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
    public Decimal_Command(Calculator calculator) : base(calculator) // Якщо у батьківському класі є конструктор, який приймає Calculator
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
    public Back_Command(Calculator calculator) : base(calculator) // Якщо у батьківському класі є конструктор, який приймає Calculator
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

    public Operator_Command(Calculator calculator, string opera) : base(calculator) // Якщо у батьківському класі є конструктор, який приймає Calculator
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
    private Calculator calculator;
    private double first;
    private double second;
    private string opera;

    public Calculate_Command(Calculator calculator) : base(calculator) // Якщо у батьківському класі є конструктор, який приймає Calculator
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        
    }

    
    public override void Execute()
    {
        calculator.calculate_expression(out _,out _,out first,out second,out opera);
    }
    
}

public class Extra_Command : Calculator_Commands
{
    private Calculator calculator;
    private string opera;

    public Extra_Command(Calculator calculator, string opera) : base(calculator)
    {
        this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        this.opera = opera;
    }

    public override void Execute()
    {
        calculator.extra_operation(opera, out _,out _);
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