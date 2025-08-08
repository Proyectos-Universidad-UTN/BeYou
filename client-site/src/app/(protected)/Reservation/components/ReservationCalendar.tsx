"use client";

import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import esLocale from "@fullcalendar/core/locales/es";

type Props = {
  onDateSelect: (date: string) => void;
};

export const ReservationCalendar = ({ onDateSelect }: Props) => {
  const handleDateClick = (info: any) => {
    // Usamos la fecha local sin cambios por UTC
    const selected = info.date.toISOString().split("T")[0];
    onDateSelect(selected);
  };

  return (
    <div className="p-4 bg-white rounded-2xl shadow-lg border">
      <FullCalendar
        plugins={[dayGridPlugin, interactionPlugin]}
        initialView="dayGridMonth"
        locale={esLocale}
        height="auto"
        selectable={true}
        dateClick={handleDateClick}
      />
    </div>
  );
};
