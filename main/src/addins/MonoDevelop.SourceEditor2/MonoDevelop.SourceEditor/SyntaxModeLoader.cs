//
// SyntaxModeLoader.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
//
// Copyright (c) 2015 Xamarin Inc. (http://xamarin.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System.IO;
using Mono.Addins;
using MonoDevelop.Ide.Editor.Highlighting;

namespace MonoDevelop.SourceEditor
{
	static class SyntaxModeLoader
	{
		static bool initialized = false;

		public static void Init ()
		{
			if (initialized)
				return;
			initialized = true;
			AddinManager.AddExtensionNodeHandler ("/MonoDevelop/SourceEditor2/SyntaxModes", OnSyntaxModeExtensionChanged);
		}
		// TODO: EditorTheme
		static void OnSyntaxModeExtensionChanged (object s, ExtensionNodeEventArgs args)
		{
			TemplateCodon codon = (TemplateCodon)args.ExtensionNode;
/*			if (args.Change == ExtensionChange.Add) {
				Mono.TextEditor.Highlighting.SyntaxModeService.AddSyntaxMode (new StreamProviderWrapper(codon));
			}*/
		}

		/*
		class StreamProviderWrapper : IStreamProvider
		{
			readonly TemplateCodon codon;

			public StreamProviderWrapper (TemplateCodon codon)
			{
				this.codon = codon;
			}

			Stream Mono.TextEditor.Highlighting.IStreamProvider.Open ()
			{
				return codon.Open ();
			}
		}*/
	}
}