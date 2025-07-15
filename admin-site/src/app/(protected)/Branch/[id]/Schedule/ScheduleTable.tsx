"use client";

import React from "react";
import { BranchSchedule } from "@/types/api-beyou";
import BranchScheduleColumns from "./BranchScheduleColumns";
import DataTableWrapper from "@/components/Table/DataTableWrapper";

interface ScheduleTableProps {
  schedules: BranchSchedule[];
}

export const ScheduleTable = ({ schedules }: ScheduleTableProps) => {
  return (
    <DataTableWrapper
      sortFieldName="ID"
      sort="desc"
      columns={BranchScheduleColumns}
      data={schedules}
    />
  )
  // return (
  //   <TableContainer component={Paper}>
  //     <Table aria-label="Horarios sucursal">
  //       <TableHead>
  //         <TableRow>
  //           <TableCell>Día</TableCell>
  //           <TableCell>Hora Inicio</TableCell>
  //           <TableCell>Hora Fin</TableCell>
  //         </TableRow>
  //       </TableHead>
  //       <TableBody>
  //         {schedules.map((schedule) => (
  //          <TableRow key={`${schedule.id ?? schedule.schedule?.id ?? Math.random()}`}>
  //             <TableCell>{schedule.schedule?.startHour ?? "-"}</TableCell>
  //             <TableCell>{schedule.schedule?.endHour ?? "-"}</TableCell>
  //           </TableRow>
  //         ))}
  //       </TableBody>
  //     </Table>
  //   </TableContainer>
  // );
};

