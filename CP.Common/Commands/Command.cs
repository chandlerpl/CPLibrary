/* 
 * Copyright (C) Pope Games, Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Author: Chandler Pope-Lewis <c.popelewis@gmail.com>
 */
using CP.Common.Logger;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Commands
{
    public abstract class Command
    {
        public string Name { get; protected set; }
        public string ProperUse { get; protected set; }
        public string Desc { get; protected set; }
        public List<string> Aliases { get; protected set; }

        public virtual CommandSystem ParentSystem { get; protected set; }
        public string CommandSystemID { get; protected set; } = "";
        protected ILogger Logger { get => ParentSystem.Logger; }

        public Command()
        {
            Init();
        }

        public abstract bool Init();

        public abstract bool Execute(object obj, List<string> args);

        public bool SetParentSystem(CommandSystem system)
        {
            if (ParentSystem != null)
                return false;

            ParentSystem = system;
            return true;
        }

        public bool CorrectLength(List<string> args, int min, int max)
        {
            return args.Count >= min && args.Count <= max;
        }

        public override string ToString()
        {
            StringBuilder message = new StringBuilder();
            message.Append("Help for: ");
            message.Append(Name);
            message.Append("\nProper Use: ");
            message.Append(ProperUse);
            message.Append("\nDescription: ");
            message.Append(Desc);
            message.Append("\nAlternatives: ");
            message.Append(Aliases.ToArray().ToString());

            return message.ToString();
        }
    }
}