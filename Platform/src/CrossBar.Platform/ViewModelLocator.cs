using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.MvvmCross.Application;
using Cirrious.MvvmCross.ExtensionMethods;
using Cirrious.MvvmCross.Interfaces.Platform.Diagnostics;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Platform.Diagnostics;
using Cirrious.MvvmCross.Plugins.Json;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using CrossBar.Platform.ViewModels.Parameters;

namespace CrossBar.Platform
{
    public class ViewModelLocator : MvxBaseViewModelLocator
    {
        public override bool TryLoad(Type viewModelType, IDictionary<string, string> parameterValueLookup, out IMvxViewModel model)
        {
            model = null;

            var provider = this.GetService<IMvxServiceProvider>();
            var method = provider.GetType().GetMethod("GetService");
            var generic = method.MakeGenericMethod(viewModelType);
            var viewModel = generic.Invoke(provider, null);

            var initializeMethod = viewModelType.GetMethod("Initialize", BindingFlags.Instance | BindingFlags.NonPublic);
            var navigateToParameter = initializeMethod.GetParameters().First();

            if (navigateToParameter.ParameterType == typeof(EmptyParameters))
            {
                initializeMethod.Invoke(viewModel, new object[]{null});
            }
            else
            {
                if (!parameterValueLookup.ContainsKey(ParametersBase.Key))
                {
                    MvxTrace.Trace(MvxTraceLevel.Error, "The navigaton was missing the parameter for {0}", ParametersBase.Key);

                    return false;
                }

                string json = parameterValueLookup[ParametersBase.Key];
                var converter = this.GetService<IMvxJsonConverter>();
                var parameters = converter.DeserializeObject(navigateToParameter.ParameterType, json);

                initializeMethod.Invoke(viewModel, new[] { parameters });
            }

            model = viewModel as IMvxViewModel;

            return true;
        }
    }
}