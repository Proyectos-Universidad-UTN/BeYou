import { ApiError } from "openapi-typescript-fetch";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import { BranchScheduleBlock } from "@/types/api-beyou";
import {
  castRequestBody,
  UseTypedApiClientBY,
} from "@/hooks/UseTypedApiClientBY";
import { isPresent } from "@/utils/util";

export const UseGetScheduleBlocksByBranchSchedule = (
  scheduleId: string | undefined
): UseQueryResult<Array<BranchScheduleBlock>, ApiError> => {
  const path = "/api/BranchSchedule/{branchScheduleId}/Block";
  const method = "get";

  const getScheduleBlocks = UseTypedApiClientBY({ path, method });

  return useQuery({
    queryKey: ["GetScheduleBlocks"],
    queryFn: async () => {
      const { data } = await getScheduleBlocks(
        castRequestBody({ branchScheduleId: Number(scheduleId) }, path, method)
      );
      return data;
    },
    retry: false,
    enabled: isPresent(scheduleId),
    staleTime: 0,
  });
};
