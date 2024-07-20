/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

namespace Meta.WitAi.Speech
{
    // Simple interface for returning speech events
    public interface ISpeechEventProvider
    {
        VoiceSpeechEvents SpeechEvents { get; }
    }
}