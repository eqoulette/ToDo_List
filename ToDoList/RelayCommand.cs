using System;
using System.Windows.Input;

namespace ToDoList
{
    public class RelayCommand : ICommand
    {
        private Action<object> perform;

        private Predicate<object> canPerform;

        private event EventHandler CanPerformChangedInternal;

        public RelayCommand(Action<object> perform)
            : this(perform, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> perform, Predicate<object> canPerform)
        {
            if (perform == null)
            {
                throw new ArgumentNullException("perform");
            }

            if (canPerform == null)
            {
                throw new ArgumentNullException("canPerform");
            }

            this.perform = perform;
            this.canPerform = canPerform;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanPerformChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanPerformChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canPerform != null && this.canPerform(parameter);
        }

        public void Execute(object parameter)
        {
            this.perform(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanPerformChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this.canPerform = _ => false;
            this.perform = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}