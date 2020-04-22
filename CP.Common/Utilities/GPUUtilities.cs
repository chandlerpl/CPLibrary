/* 
 * Copyright (C) Pope Games, Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Author: Chandler Pope-Lewis <c.popelewis@gmail.com>
 */
using CP.CommonCUDA;

namespace CP.Common.Utilities
{
    public static class GPUUtilities
    {
        static GpuCap LatestGPUQuery;

        public static bool CheckCUDACapability()
        {
            GpuCap gpu = CUDAUtilities.GetCapabilities();
            LatestGPUQuery = gpu;
            if (gpu.DeviceCount > 0 && !gpu.QueryFailed)
            {
                return true;
            }

            return false;
        }

        public static int GetStrongestCudaGpu()
        {
            return LatestGPUQuery.StrongestDeviceId;
        }
    }
}