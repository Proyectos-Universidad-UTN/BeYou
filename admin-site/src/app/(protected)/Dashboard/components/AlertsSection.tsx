// components/AlertsSection.tsx

import React from 'react';
import { FaExclamationTriangle, FaCalendarTimes, FaInfoCircle } from 'react-icons/fa';
import { format } from 'date-fns';
import { es } from 'date-fns/locale';

// Tipos para las props del componente
interface AlertsSectionProps {
  pendingReservations: { id: number; customerName: string; date: string; status: string }[];
  cancelledReservations: { id: number; customerName: string; date: string; status: string }[];
}

export default function AlertsSection({ pendingReservations, cancelledReservations }: AlertsSectionProps) {
  const hasAlerts = pendingReservations.length > 0 || cancelledReservations.length > 0;

  if (!hasAlerts) {
    return null; // No muestra nada si no hay alertas
  }

  return (
    <div className="grid grid-cols-1 lg:grid-cols-2 gap-6 p-6 rounded-lg bg-white shadow-lg border-l-4 border-yellow-500">
      {pendingReservations.length > 0 && (
        <div className="bg-yellow-50 border-l-4 border-yellow-400 p-4 rounded-md">
          <div className="flex items-center gap-3 mb-2">
            <FaExclamationTriangle size={24} className="text-yellow-500" />
            <h4 className="font-semibold text-lg text-yellow-800">
              {pendingReservations.length} Reserva{pendingReservations.length > 1 ? 's' : ''} Pendiente{pendingReservations.length > 1 ? 's' : ''}
            </h4>
          </div>
          <p className="text-sm text-yellow-700">
            Asegúrate de revisar y confirmar estas reservas.
          </p>
          <ul className="mt-3 space-y-2 text-sm text-gray-800">
            {pendingReservations.slice(0, 3).map(r => (
              <li key={r.id} className="flex items-center gap-2">
                <FaInfoCircle className="text-yellow-600" />
                <span>
                  **{r.customerName}** para el {format(new Date(r.date), 'dd MMMM yyyy', { locale: es })}.
                </span>
              </li>
            ))}
          </ul>
        </div>
      )}

      {cancelledReservations.length > 0 && (
        <div className="bg-red-50 border-l-4 border-red-400 p-4 rounded-md">
          <div className="flex items-center gap-3 mb-2">
            <FaCalendarTimes size={24} className="text-red-500" />
            <h4 className="font-semibold text-lg text-red-800">
              {cancelledReservations.length} Reserva{cancelledReservations.length > 1 ? 's' : ''} Cancelada{cancelledReservations.length > 1 ? 's' : ''}
            </h4>
          </div>
          <p className="text-sm text-red-700">
            Puedes contactar al cliente o liberar el cupo.
          </p>
          <ul className="mt-3 space-y-2 text-sm text-gray-800">
            {cancelledReservations.slice(0, 3).map(r => (
              <li key={r.id} className="flex items-center gap-2">
                <FaInfoCircle className="text-red-600" />
                <span>
                  **{r.customerName}** para el {format(new Date(r.date), 'dd MMMM yyyy', { locale: es })}.
                </span>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}