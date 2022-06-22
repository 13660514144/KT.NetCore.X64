using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Common.Event
{
    public class MessageResources
    {
        public static string CannotRegisterSameCommandTwice = "Cannot register the same command twice in the same CompositeCommand.";
        public static string CyclicDependencyFound = "At least one cyclic dependency has been found in the module catalog.Cycles in the module dependencies must be avoided.";
        public static string DefaultDebugLoggerPattern = "{1}:{2}. Priority:{3}. Timestamp:{0:u}.";
        public static string DelegateCommandDelegatesCannotBeNull = "Neither the executeMethod nor the canExecuteMethod delegates can be null.";
        public static string DelegateCommandInvalidGenericPayloadType = "T for DelegateCommand<T> is not an object nor Nullable.";
        public static string DependencyForUnknownModule = "Cannot add dependency for unknown module {0}";
        public static string DependencyOnMissingModule = "A module declared a dependency on another module which is not declared to be loaded.Missing module(s): {0}";
        public static string DuplicatedModule = "A duplicated module with name {0} has been found by the loader.";
        public static string EventAggregatorNotConstructedOnUIThread = "To use the UIThread option for subscribing, the EventAggregator must be constructed on the UI thread.";
        public static string FailedToLoadModule = "An exception occurred while initializing module '{0}'. " +
                                                    "- The exception message was: {2} " +
                                                    "- The Assembly that the module was trying to be loaded from was:{1} " +
                                                    "Check the InnerException property of the exception for more information. " +
                                                    "If the exception occurred while creating an object in a DI container," +
                                                    " you can exception.GetRootException() to help locate the root cause of the problem.";
        public static string FailedToLoadModuleNoAssemblyInfo = "An exception occurred while initializing module '{0}'.  " +
                                                                "- The exception message was: {1} " +
                                                                "Check the InnerException property of the exception for more information. " +
                                                                "If the exception occurred while creating an object in a DI container, " +
                                                                "you can exception.GetRootException() to help locate the root cause of the problem.";
        public static string FailedToRetrieveModule = "Failed to load type for module {0}. Error was: {1}.";
        public static string InvalidDelegateRerefenceTypeException = "Invalid Delegate Reference Type Exception";
        public static string InvalidPropertyNameException = "The entity does not contain a property with that name";
        public static string ModuleDependenciesNotMetInGroup = "Module {0} depends on other modules that don't belong to the same group.";
        public static string PropertySupport_ExpressionNotProperty_Exception = "The member access expression does not access a property.";
        public static string PropertySupport_NotMemberAccessExpression_Exception = "The expression is not a member access expression.";
        public static string PropertySupport_StaticExpression_Exception = "The referenced property is a static property.";
        public static string StartupModuleDependsOnAnOnDemandModule = "Module {0} is marked for automatic initialization when the application starts," +
                                                                        " but it depends on modules that are marked as OnDemand initialization.To fix this error, " +
                                                                        "mark the dependency modules for InitializationMode = WhenAvailable, " +
                                                                        "or remove this validation by extending the ModuleCatalog class.";
        public static string StringCannotBeNullOrEmpty = "The provided String argument {0} must not be null or empty.";


    }
}
