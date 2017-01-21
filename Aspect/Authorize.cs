using System;
using System.Linq;
using PostSharp.Aspects;
using RowLevelSecurity.Context;

namespace RowLevelSecurity.Aspect
{
    [Serializable]
    public class Authorize : OnMethodBoundaryAspect
    {
        public string Username { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            string userName = null;
            var parameters = args.Method.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
                if (parameters[i].Name.ToLower().Equals("username"))
                    userName = (string) args.Arguments.GetArgument(i);

            var context = args.Arguments.OfType<RowSecurityContext>().FirstOrDefault();
            if (userName == null || context == null)
                throw new ArgumentException("Argument usrename and context are required");

            context.Authorize(userName);
        }
    }
}