using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using CalculatorMVVM.Model;

namespace CalculatorMVVM.ViewModel;

/// <summary>
/// Basic calculator application.
/// </summary>

public class CalculatorViewModel : INotifyPropertyChanged
{
    // registre for holding data. RegC is reserved for holding sums of 
    // calculations. RegA & RegB is temp containers for holding user given values.
    private double _regA, _regB, _regC;

    public double regC
    {
        get { return _regC; }
        set
        {
            _regC = value;
            OnPropertyChanged();
        }
    }
    private Operator _operator; 
    private int? activeReg;

    private void Calculate()
    {

        switch (_operator)
        {
            
            case Operator.Divide:
                regC = _regA / _regB;
                break; 
            
            case Operator.Multiply:
                regC = _regA * _regB;
                break; 
            
            case Operator.Minus:
                regC = _regA - _regB;
                break; 
            
            case Operator.Plus:
                regC = _regA + _regB;
                break;
        }
        
    }
    

    #region Commands
    
    public ICommand CommandNum => new IcommandBase.ICommandBase((Object commandPara) =>
        {
            {
                Debug.WriteLine($"Object recived: {commandPara}");
                if (commandPara as Button != null)
                {
                    Button btn = (Button)commandPara;
                    regC += double.Parse(btn.Content.ToString());
                }
                
                Debug.WriteLine("regC: " + regC);

            }
            
        });

    public ICommand CommandPlus => new IcommandBase.ICommandBase((Object commandPara) =>
    {
        _operator = Operator.Plus;
        
    });

    public ICommand CommandMinus => new IcommandBase.ICommandBase((Object commandPara) =>
    {
        _operator = Operator.Minus;
    });

    public ICommand CommandDivide => new IcommandBase.ICommandBase((Object commandPara) =>
    {
        {
            _operator = Operator.Divide;
        }
    });

    public ICommand CommandEqual => new IcommandBase.ICommandBase((Object commandPara) => {
        {
            Calculate();
            _operator = Operator.Equal;
            Debug.WriteLine(_regC.ToString());
        }
        
    });

    
    public ICommand CommandMultiply => new IcommandBase.ICommandBase((Object commandPara) => {
        {
            _operator = Operator.Multiply;
        }
        
    }); 
    
    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    
}