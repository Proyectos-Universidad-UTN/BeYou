// components/DateRangeFilter.tsx

"use client";

import React, { useState } from 'react';

interface DateRangeFilterProps {
  onDateRangeChange: (startDate: Date | null, endDate: Date | null) => void;
}

export default function DateRangeFilter({ onDateRangeChange }: DateRangeFilterProps) {
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);

  const handleStartDateChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newStartDate = event.target.value ? new Date(event.target.value) : null;
    setStartDate(newStartDate);
    onDateRangeChange(newStartDate, endDate);
  };

  const handleEndDateChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newEndDate = event.target.value ? new Date(event.target.value) : null;
    setEndDate(newEndDate);
    onDateRangeChange(startDate, newEndDate);
  };

  return (
    <div className="flex items-center gap-4">
      <label className="text-sm font-medium text-gray-700">Desde:</label>
      <input
        type="date"
        onChange={handleStartDateChange}
        className="p-2 border rounded-md shadow-sm"
      />
      <label className="text-sm font-medium text-gray-700">Hasta:</label>
      <input
        type="date"
        onChange={handleEndDateChange}
        className="p-2 border rounded-md shadow-sm"
      />
    </div>
  );
}