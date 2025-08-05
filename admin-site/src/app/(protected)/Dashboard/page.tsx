// app/page.tsx (código actualizado)

"use client";

import React, { useState, useMemo } from 'react';
import { FaCalendarAlt, FaCheckCircle, FaExclamationCircle, FaTimesCircle, FaPercent, FaRegClock, FaInfoCircle } from 'react-icons/fa';
import { Pie } from 'react-chartjs-2';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';
import { format } from 'date-fns';
import { es } from 'date-fns/locale';
import { Reservation } from "@/types/api-beyou";
import DateRangeFilter from './components/DateRangeFilter';
import AlertsSection from './components/AlertsSection';

ChartJS.register(ArcElement, Tooltip, Legend);

// --- Tipos e Interfaces ---
interface DashboardStats {
  totalRecent: number;
  totalConfirmed: number;
  totalPending: number;
  totalCancelled: number;
  confirmationRate: string;
  pendingReservations: { id: number; customerName: string; date: string; status: string }[];
  cancelledReservations: { id: number; customerName: string; date: string; status: string }[];
  recentReservations: { id: number; customerName: string; date: string; status: string }[];
  upcomingAppointments: { id: number; customerName: string; date: string; time: string; status: string; }[];
  recentActivity: { id: number; customerName: string; action: 'creada' | 'confirmada' | 'cancelada'; date: string; }[];
}

interface StatCardProps {
  title: string;
  value: number | string | undefined;
  icon: React.ReactNode;
  bgColor: string;
  description?: string;
}

// --- Hooks Personalizados ---
import { UseGetReservations } from '@/hooks/api-beyou/reservation/UseGetReservations'; // Asegúrate de que la ruta sea correcta

function useDashboardStats(allReservations: Array<Reservation> | undefined, startDate: Date | null, endDate: Date | null) {
  const stats = useMemo(() => {
    if (!allReservations || allReservations.length === 0) {
      return null;
    }

    const filteredReservations = allReservations.filter(r => {
      // Filtramos solo las reservas que tienen id y date definidos
      if (!r.date || r.id === undefined) return false;
      const reservationDate = new Date(r.date);
      const isAfterStartDate = !startDate || reservationDate >= startDate;
      const isBeforeEndDate = !endDate || reservationDate <= endDate;
      return isAfterStartDate && isBeforeEndDate;
    });

    const today = new Date();
    
    // Ahora, cuando filtramos por estado, ya sabemos que id y date existen.
    const pendingReservations = filteredReservations
      .filter(r => r.status === 'P')
      .map(r => ({
        id: r.id!, // Usamos ! porque ya filtramos que no es undefined
        customerName: r.customerName!,
        date: r.date!,
        status: r.status!
      }));
      
    const cancelledReservations = filteredReservations
      .filter(r => r.status === 'X')
      .map(r => ({
        id: r.id!,
        customerName: r.customerName!,
        date: r.date!,
        status: r.status!
      }));
      
    const confirmedReservations = filteredReservations.filter(r => r.status === 'C');

    const totalReservations = filteredReservations.length;
    const confirmationRate = totalReservations > 0
      ? ((confirmedReservations.length / totalReservations) * 100).toFixed(1) + '%'
      : '0%';

    const recentReservations = filteredReservations
      .filter(r => r.date)
      .slice(0, 3)
      .map(r => ({
        id: r.id!,
        customerName: r.customerName!,
        date: r.date!,
        status: r.status === 'C' ? 'Confirmado' : r.status === 'P' ? 'Pendiente' : 'Cancelado',
      }));

    const upcomingAppointments = filteredReservations
      .filter(r => r.date && new Date(r.date) >= today && r.status === 'C')
      .sort((a, b) => new Date(a.date!).getTime() - new Date(b.date!).getTime())
      .slice(0, 5)
      .map(r => ({
        id: r.id!,
        customerName: r.customerName!,
        date: r.date!,
        time: format(new Date(r.date!), 'HH:mm', { locale: es }),
        status: 'Confirmado',
      }));

    const recentActivity = filteredReservations
      .filter(r => r.date)
      .sort((a, b) => new Date(b.date!).getTime() - new Date(a.date!).getTime())
      .slice(0, 5)
      .map(r => {
        let actionText: 'creada' | 'confirmada' | 'cancelada';
        switch (r.status) {
          case 'C':
            actionText = 'confirmada';
            break;
          case 'P':
            actionText = 'creada';
            break;
          case 'X':
            actionText = 'cancelada';
            break;
          default:
            actionText = 'creada';
        }
        return {
          id: r.id!,
          customerName: r.customerName!,
          action: actionText,
          date: r.date!,
        };
      });

    return {
      totalRecent: totalReservations,
      totalConfirmed: confirmedReservations.length,
      totalPending: pendingReservations.length,
      totalCancelled: cancelledReservations.length,
      confirmationRate: confirmationRate,
      pendingReservations,
      cancelledReservations,
      recentReservations,
      upcomingAppointments,
      recentActivity,
    };
  }, [allReservations, startDate, endDate]);

  return { stats };
}

// --- Componentes Reutilizables ---
const StatCard = ({ title, value, icon, bgColor, description }: StatCardProps) => (
  <div className={`p-6 rounded-lg shadow-lg text-white ${bgColor} transform transition-transform duration-300 hover:scale-105`}>
    <div className="flex items-center justify-between mb-2">
      <h2 className="text-sm font-light uppercase">{title}</h2>
      {icon}
    </div>
    <p className="text-4xl font-bold">{value}</p>
    {description && <p className="text-xs mt-2 opacity-80">{description}</p>}
  </div>
);

const EmptyState = ({ message }: { message: string }) => (
    <div className="flex flex-col items-center justify-center p-8 bg-white rounded-lg shadow-lg">
      <FaRegClock size={48} className="text-gray-400 mb-4" />
      <p className="text-lg text-gray-600">{message}</p>
    </div>
);

// --- Componente Principal ---
export default function Page() {
  const [startDate, setStartDate] = useState<Date | null>(null);
  const [endDate, setEndDate] = useState<Date | null>(null);

  const { data: allReservations, isLoading } = UseGetReservations();
  
  const { stats } = useDashboardStats(allReservations, startDate, endDate);

  const handleDateRangeChange = (newStartDate: Date | null, newEndDate: Date | null) => {
    setStartDate(newStartDate);
    setEndDate(newEndDate);
  };

  if (isLoading) {
    return (
      <div className="flex flex-1 flex-col gap-4 p-8 bg-gray-100">
        <p className="text-center text-lg text-gray-700">Cargando datos del dashboard...</p>
      </div>
    );
  }

  if (!stats) {
    return (
        <div className="flex flex-1 flex-col gap-4 p-8 bg-gray-100">
          <h1 className="text-3xl font-extrabold text-gray-800 border-b-2 border-gray-300 pb-2">
            Dashboard de Reservas
          </h1>
          <EmptyState message="Aún no hay reservas para mostrar. ¡Empieza a recibir reservas!" />
        </div>
      );
  }

  const pieData = {
    labels: ['Confirmadas', 'Pendientes', 'Canceladas'],
    datasets: [
      {
        data: [stats.totalConfirmed, stats.totalPending, stats.totalCancelled],
        backgroundColor: ['#10B981', '#F59E0B', '#EF4444'],
        borderColor: ['#ffffff', '#ffffff', '#ffffff'],
        borderWidth: 1,
      },
    ],
  };

  return (
    <div className="flex flex-1 flex-col gap-8 p-8 bg-gray-100 min-h-screen">
      <div className="flex justify-between items-center flex-wrap gap-4 border-b-2 border-gray-300 pb-2">
        <h1 className="text-3xl font-extrabold text-gray-800">
          Dashboard de Reservas
        </h1>
        <DateRangeFilter onDateRangeChange={handleDateRangeChange} />
      </div>

      {/* Sección de Tarjetas de Estadísticas */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-5 gap-6">
        <StatCard
          title="Reservas Recientes"
          value={stats.totalRecent}
          icon={<FaCalendarAlt size={24} />}
          bgColor="bg-gradient-to-br from-blue-500 to-blue-700"
          description="Total de reservas recibidas."
        />
        <StatCard
          title="Reservas Confirmadas"
          value={stats.totalConfirmed}
          icon={<FaCheckCircle size={24} />}
          bgColor="bg-gradient-to-br from-green-500 to-green-700"
          description="Total de reservas confirmadas."
        />
        <StatCard
          title="Reservas Pendientes"
          value={stats.totalPending}
          icon={<FaExclamationCircle size={24} />}
          bgColor="bg-gradient-to-br from-yellow-400 to-yellow-600"
          description="Reservas a la espera de confirmación."
        />
        <StatCard
          title="Reservas Canceladas"
          value={stats.totalCancelled}
          icon={<FaTimesCircle size={24} />}
          bgColor="bg-gradient-to-br from-red-500 to-red-700"
          description="Reservas que fueron canceladas."
        />
        <StatCard
          title="Tasa de Confirmación"
          value={stats.confirmationRate}
          icon={<FaPercent size={24} />}
          bgColor="bg-gradient-to-br from-purple-500 to-purple-700"
          description="Porcentaje de reservas confirmadas."
        />
      </div>

      {/* Sección de Gráfico y Próximas Citas */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-6">
        {/* Gráfico de Torta */}
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold mb-4 text-gray-700">Distribución de Reservas</h3>
          <div className="h-64 flex items-center justify-center">
            <Pie data={pieData} />
          </div>
        </div>

        {/* Próximas Citas */}
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold mb-4 text-gray-700">Próximas Citas</h3>
          {stats.upcomingAppointments.length > 0 ? (
            <ul className="space-y-4">
              {stats.upcomingAppointments.map((r) => (
                <li
                  key={r.id}
                  className="flex items-center p-4 bg-blue-50 rounded-lg border border-blue-200"
                >
                  <FaCalendarAlt className="text-blue-500 mr-4" size={20} />
                  <div className="flex-1">
                    <p className="font-medium text-blue-800">{r.customerName}</p>
                    <p className="text-sm text-blue-600">
                      {format(new Date(r.date), 'dd MMMM yyyy HH:mm', { locale: es })}
                    </p>
                  </div>
                </li>
              ))}
            </ul>
          ) : (
            <EmptyState message="No hay próximas citas confirmadas." />
          )}
        </div>
      </div>

      {/* Sección de Últimas Reservas y Actividad Reciente */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-6">
        {/* Últimas Reservas */}
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold mb-4 text-gray-700">Últimas Reservas</h3>
          <ul className="space-y-4">
            {stats.recentReservations.length > 0 ? (
              stats.recentReservations.map((r) => (
                <li
                  key={r.id}
                  className="flex items-center p-4 bg-gray-50 rounded-lg border border-gray-200 transition-shadow duration-300 hover:shadow-md"
                >
                  <div className="flex-1">
                    <p className="font-medium text-gray-800">{r.customerName}</p>
                    <p className="text-sm text-gray-500">
                      {format(new Date(r.date), 'dd MMMM yyyy', { locale: es })}
                    </p>
                  </div>
                  <span
                    className={`text-sm font-semibold px-3 py-1 rounded-full capitalize ${
                      r.status === 'Confirmado'
                        ? 'bg-green-100 text-green-800'
                        : r.status === 'Pendiente'
                        ? 'bg-yellow-100 text-yellow-800'
                        : 'bg-red-100 text-red-800'
                    }`}
                  >
                    {r.status}
                  </span>
                </li>
              ))
            ) : (
                <EmptyState message="No hay reservas recientes para mostrar." />
            )}
          </ul>
        </div>
        
        {/* Actividad Reciente */}
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold mb-4 text-gray-700">Actividad Reciente</h3>
          {stats.recentActivity.length > 0 ? (
            <ul className="space-y-4">
              {stats.recentActivity.map((a) => (
                <li
                  key={a.id}
                  className="flex items-start p-4 bg-gray-50 rounded-lg border border-gray-200"
                >
                  <FaInfoCircle className="text-gray-500 mr-4 mt-1" size={16} />
                  <div className="flex-1">
                    <p className="text-gray-800">
                      <span className="font-medium">{a.customerName}</span> ha{" "}
                      <span className={`font-semibold ${a.action === 'cancelada' ? 'text-red-600' : 'text-green-600'}`}>
                        {a.action}
                      </span>{" "}
                      una reserva.
                    </p>
                    <p className="text-xs text-gray-500 mt-1">
                      {format(new Date(a.date), 'dd MMMM yyyy, HH:mm', { locale: es })}
                    </p>
                  </div>
                </li>
              ))}
            </ul>
          ) : (
            <EmptyState message="No hay actividad reciente para mostrar." />
          )}
        </div>
      </div>
       <AlertsSection 
        pendingReservations={stats.pendingReservations} 
        cancelledReservations={stats.cancelledReservations} 
      />
    </div>
  );
}