//
// GrayOutMarker.cs
//
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
//
// Copyright (c) 2014 Xamarin Inc. (http://xamarin.com)
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

using System;
using Mono.TextEditor;
using System.Collections.Generic;
using MonoDevelop.Components;
using MonoDevelop.Core.Text;
using MonoDevelop.Ide.Editor.Highlighting;

namespace MonoDevelop.SourceEditor
{
	class GrayOutMarker : UnderlineTextSegmentMarker, IChunkMarker, MonoDevelop.Ide.Editor.IGenericTextSegmentMarker
	{
		public GrayOutMarker (ISegment segment) : base ("", segment)
		{
		}

		public override void Draw (MonoTextEditor editor, Cairo.Context cr, LineMetrics metrics, int startOffset, int endOffset)
		{
			// nothing (is drawn using chunk marker)
		}

		#region IChunkMarker implementation

		void IChunkMarker.TransformChunks (List<MonoDevelop.Ide.Editor.Highlighting.ColoredSegment> chunks)
		{
			int markerStart = Segment.Offset;
			int markerEnd = Segment.EndOffset;
			for (int i = 0; i < chunks.Count; i++) {
				var chunk = chunks [i];
				if (chunk.EndOffset < markerStart || markerEnd <= chunk.Offset) 
					continue;
				if (chunk.Offset == markerStart && chunk.EndOffset == markerEnd)
					return;
				if (chunk.Offset < markerStart && chunk.EndOffset > markerEnd) {
					var newChunk = new MonoDevelop.Ide.Editor.Highlighting.ColoredSegment (chunk.Offset, markerStart - chunk.Offset, chunk.ColorStyleKey);
					chunks [i] = new MonoDevelop.Ide.Editor.Highlighting.ColoredSegment (chunk.Offset + newChunk.Length, chunk.Length - newChunk.Length, chunk.ColorStyleKey);
					chunks.Insert (i, newChunk);
					continue;
				}
			}
		}

		void IChunkMarker.ChangeForeColor (MonoTextEditor editor, MonoDevelop.Ide.Editor.Highlighting.ColoredSegment chunk, ref Cairo.Color color)
		{
			if (Debugger.DebuggingService.IsDebugging)
				return;
			int markerStart = Segment.Offset;
			int markerEnd = Segment.EndOffset;
			if (chunk.EndOffset <= markerStart || markerEnd <= chunk.Offset) 
				return;
			
			var bgc = (Cairo.Color)SyntaxModeService.GetColor (editor.EditorTheme, ThemeSettingColors.Background);
			double alpha = 0.6;
			color = new Cairo.Color (
				color.R * alpha + bgc.R * (1.0 - alpha),
				color.G * alpha + bgc.G * (1.0 - alpha),
				color.B * alpha + bgc.B * (1.0 - alpha)
			);
		}
		#endregion

		event EventHandler<MonoDevelop.Ide.Editor.TextMarkerMouseEventArgs> MonoDevelop.Ide.Editor.ITextSegmentMarker.MousePressed {
			add {
				throw new NotSupportedException ();
			}
			remove {
				throw new NotSupportedException ();
			}
		}

		event EventHandler<MonoDevelop.Ide.Editor.TextMarkerMouseEventArgs> MonoDevelop.Ide.Editor.ITextSegmentMarker.MouseHover {
			add {
				throw new NotSupportedException ();
			}
			remove {
				throw new NotSupportedException ();
			}
		}

		object MonoDevelop.Ide.Editor.ITextSegmentMarker.Tag {
			get;
			set;
		}

		MonoDevelop.Ide.Editor.TextSegmentMarkerEffect MonoDevelop.Ide.Editor.IGenericTextSegmentMarker.Effect {
			get {
				return MonoDevelop.Ide.Editor.TextSegmentMarkerEffect.GrayOut;
			}
		}

		HslColor MonoDevelop.Ide.Editor.IGenericTextSegmentMarker.Color {
			get {
				throw new NotSupportedException ();
			}
			set {
				throw new NotSupportedException ();
			}
		}
	}
}
