﻿// Copyright (C) Microsoft. All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using Microsoft.VisualStudio.Text;
using System.Text;

namespace DevSkim
{
    public class DevSkimError
    {
        public readonly SnapshotSpan Span;
        public readonly Rule Rule;

        // This is used by SecurityErrorsSnapshot.TranslateTo() to map this error to the corresponding error in the next snapshot.
        public int NextIndex = -1;

        public DevSkimError(SnapshotSpan span, Rule rule)
        {
            this.Span = span;
            this.Rule = rule;
        }
        
        public static DevSkimError Clone(DevSkimError error)
        {
            return new DevSkimError(error.Span, error.Rule);
        }

        public static DevSkimError CloneAndTranslateTo(DevSkimError error, ITextSnapshot newSnapshot)
        {
            var newSpan = error.Span.TranslateTo(newSnapshot, SpanTrackingMode.EdgeExclusive);

            // We want to only translate the error if the length of the error span did not change (if it did change, it would imply that
            // there was some text edit inside the error and, therefore, that the error is no longer valid).
            return (newSpan.Length == error.Span.Length)
                   ? new DevSkimError(newSpan, error.Rule)
                   : null;
        }
    }
}
