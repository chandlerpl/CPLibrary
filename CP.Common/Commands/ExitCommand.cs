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
	class ExitCommand : CPCommand
	{
		public override bool Init()
		{
			Name = "Exit";
			Desc = "Exits the application";
			Aliases = new List<string>() { "exit" };
			ProperUse = "exit";

			return true;
		}

		public override bool Execute(object obj, List<string> args)
		{
			Environment.Exit(0);

			return true;
		}
	}
}
