// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Runtime.Async;
using Dolittle.Runtime.Events.Processing.Streams;

namespace Dolittle.Runtime.Events.Processing
{
    public class in_memory_stream_processor_state_repository : IResilientStreamProcessorStateRepository
    {
        readonly IDictionary<StreamProcessorId, IStreamProcessorState> states = new NullFreeDictionary<StreamProcessorId, IStreamProcessorState>();

        public void Dispose()
        {
        }

        public Task Persist(IStreamProcessorId streamProcessorId, IStreamProcessorState streamProcessorState, CancellationToken cancellationToken)
        {
            states[streamProcessorId as StreamProcessorId] = streamProcessorState;
            return Task.CompletedTask;
        }

        public Task<Try<IStreamProcessorState>> TryGetFor(IStreamProcessorId streamProcessorId, CancellationToken cancellationToken)
        {
            if (states.ContainsKey(streamProcessorId as StreamProcessorId)) return Task.FromResult(new Try<IStreamProcessorState>(true, states[streamProcessorId as StreamProcessorId]));
            else return Task.FromResult(new Try<IStreamProcessorState>(false, null));
        }
    }
}