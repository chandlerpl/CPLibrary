/* 
 * Copyright (C) Pope Games, Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Author: Chandler Pope-Lewis <c.popelewis@gmail.com>
 */
using CP.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Tests
{
    class HelpCommand : Command
    {
        public override bool Init()
        {
            Name = "Help";
            Desc = "Shows all available commands.";
            Aliases = new List<string>() { "help", "h" };
            ProperUse = "help";

            return true;
        }

        public override bool Execute(object obj, List<string> args)
        {
			if (args == null || args.Count == 1)
			{
				StringBuilder message = new StringBuilder();

				foreach (Command command in ParentSystem.Commands.Values)
				{
					message.Append(command.Name);
					message.Append(" - ");
					message.Append(command.Desc);
					message.Append("\n");
				}

				Logger.WriteLine(message.ToString());
			} else
			{
				string arg = args[1];
				if(ParentSystem.Commands.ContainsKey(arg.ToLower()))
				{
					Logger.WriteLine(ParentSystem.Commands[arg.ToLower()].ToString());
				}
			}

			return true;
		}
    }
}
