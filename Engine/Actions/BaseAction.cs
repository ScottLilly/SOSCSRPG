using System;
using Engine.Models;

namespace Engine.Actions
{
    public abstract class BaseAction
    {
        protected readonly GameItem _itemInUse;

        public event EventHandler<string> OnActionPerformed;

        protected BaseAction(GameItem itemInUse)
        {
            _itemInUse = itemInUse;
        }

        protected void ReportResult(string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}