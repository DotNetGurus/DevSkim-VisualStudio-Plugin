﻿//------------------------------------------------------------------------------
// <copyright file="VSPackage.cs" company="Microsoft">
//      Copyright(C) Microsoft.All rights reserved.
//      Licensed under the MIT License.See LICENSE.txt in the project root for license information.
// </copyright>
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using EnvDTE80;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;

namespace DevSkim
{
    /// <summary>
    /// This class implements a Visual Studio package that is registered for the Visual Studio IDE.
    /// The package class uses a number of registration attributes to specify integration parameters.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideOptionPage(typeof(OptionsDialogPage), "DevSkim Options", "General", 100, 101, supportsAutomation: true)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [Guid(GuidStrings.GuidPackage)]
    class VSPackage : Package
    {
        /// <summary>
        /// Initialization of the package.  This is where you should put all initialization
        /// code that depends on VS services.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // TODO: add initialization code here
            
            // Initialize shared components
            DTE = GetService(typeof(DTE)) as DTE2;
        }

        /// <summary>
        /// Get active open solution
        /// </summary>
        /// <returns>Open solution</returns>
        public static Solution GetSolution()
        {
            if (DTE.Solution == null)
                return null;

            return DTE.Solution;
        }

        /// <summary>
        /// Gets project name that contains provided file
        /// </summary>
        /// <param name="fileName">File that belongs to project</param>
        /// <returns>Project name</returns>
        public static string GetProjectName(string fileName)
        {
            string result = null;            
            if (DTE.Solution != null)
            {
                ProjectItem prj= DTE.Solution.FindProjectItem(fileName);
                if (prj != null)
                    result = prj.ContainingProject.Name;
            }

            return result;
        }

        private static DTE2 DTE { get; set; }
    }
}
