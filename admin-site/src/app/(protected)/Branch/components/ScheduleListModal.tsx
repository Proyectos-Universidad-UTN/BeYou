'use client';

import { Dialog } from '@headlessui/react';
import { useEffect } from 'react';
import { UseGetBranchSchedules } from '@/hooks/api-beyou/branch/schedule/UseGetBranchSchedules';
import { CircularLoadingProgress } from '@/components/LoadingProgress/CircularLoadingProcess';

interface Props {
  branchId: number | null;
  open: boolean;
  onClose: () => void;
}

type BranchScheduleItem = {
  scheduleId: number;
  schedule: {
    day?: number;
    startHour?: string;
    endHour?: string;
  };
};

export const ScheduleListModal = ({ branchId, open, onClose }: Props) => {
  const { data, isLoading, refetch } = UseGetBranchSchedules(
    branchId ? branchId.toString() : undefined
  );

  useEffect(() => {
    if (open && branchId) {
      refetch();
    }
  }, [open, branchId]);

  const getDayLabel = (day?: number) => {
    const days = [
      'Domingo',
      'Lunes',
      'Martes',
      'Miércoles',
      'Jueves',
      'Viernes',
      'Sábado',
    ];
    return typeof day === 'number' ? days[day % 7] : 'Desconocido';
  };

  return (
    <Dialog open={open} onClose={onClose} className="fixed z-50 inset-0 overflow-y-auto">
      <div className="flex items-center justify-center min-h-screen px-4">
        <Dialog.Panel className="bg-white p-6 rounded-lg shadow-xl max-w-md w-full">
          <Dialog.Title className="text-lg font-bold mb-4">
            Horarios asignados a la sucursal
          </Dialog.Title>

          {isLoading ? (
            <CircularLoadingProgress />
          ) : data && (data as BranchScheduleItem[]).length > 0 ? (
            <ul className="space-y-2 max-h-[300px] overflow-y-auto">
              {(data as BranchScheduleItem[]).map((item) => (
                <li key={item.scheduleId} className="border p-2 rounded">
                  <strong>{getDayLabel(item.schedule?.day)}</strong>: {item.schedule?.startHour} - {item.schedule?.endHour}
                </li>
              ))}
            </ul>
          ) : (
            <p>No hay horarios asignados a esta sucursal.</p>
          )}

          <div className="mt-4 text-right">
            <button
              onClick={onClose}
              className="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700"
            >
              Cerrar
            </button>
          </div>
        </Dialog.Panel>
      </div>
    </Dialog>
  );
};
