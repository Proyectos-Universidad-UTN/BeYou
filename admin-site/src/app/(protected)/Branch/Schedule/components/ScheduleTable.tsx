"use client";

import React from "react";
import { Box, Typography, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from "@mui/material";
import { BranchSchedule } from "@/types/api-beyou";

interface ScheduleTableProps {
  branchId: number;
  schedules: BranchSchedule[];
}

export const ScheduleTable = ({ branchId, schedules }: ScheduleTableProps) => {
  if (!schedules.length) {
    return (
      <Box textAlign="center" mt={4}>
        <Typography variant="body1" color="textSecondary">
          No hay horarios asignados a esta sucursal.
        </Typography>
      </Box>
    );
  }

  return (
    <TableContainer component={Paper}>
      <Table aria-label="Horarios sucursal">
        <TableHead>
          <TableRow>
            <TableCell>Día</TableCell>
            <TableCell>Hora Inicio</TableCell>
            <TableCell>Hora Fin</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {schedules.map((schedule) => (
           <TableRow key={`${schedule.id ?? schedule.schedule?.id ?? Math.random()}`}>
              <TableCell>{schedule.schedule?.startHour ?? "-"}</TableCell>
              <TableCell>{schedule.schedule?.endHour ?? "-"}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

// Función auxiliar para traducir el día a español
const getDayInSpanish = (day: string): string => {
  const daysMap: Record<string, string> = {
    Monday: "Lunes",
    Tuesday: "Martes",
    Wednesday: "Miércoles",
    Thursday: "Jueves",
    Friday: "Viernes",
    Saturday: "Sábado",
    Sunday: "Domingo",
  };
  return daysMap[day] || day;
};
