using System.Web.Mvc;
using Castle.Components.Validator;

namespace TimeTracker.Web.Code
{
	public class ValidatingModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var result = base.BindModel(controllerContext, bindingContext);
			if (result == null)
			{
				return result;
			}
			
			var runner = new ValidatorRunner(new CachedValidationRegistry());
			if (runner.IsValid(result))
			{
				return result;
			}

			var summary = runner.GetErrorSummary(result);
			var modelState = bindingContext.ModelState;
			foreach (var invalidProperty in summary.InvalidProperties)
			{
				foreach (var error in summary.GetErrorsForProperty(invalidProperty))
				{
					modelState.AddModelError(invalidProperty, error);
				}
			}

			return result;
		}
	}
}