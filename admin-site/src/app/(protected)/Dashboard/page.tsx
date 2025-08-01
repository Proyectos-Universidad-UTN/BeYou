"use client";

import React, { useEffect, useState } from 'react';
import { FaCalendarAlt, FaCheckCircle, FaExclamationCircle, FaTimesCircle } from 'react-icons/fa';
import { Pie } from 'react-chartjs-2';
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js';

// Registrar los componentes de Chart.js que vamos a usar
ChartJS.register(ArcElement, Tooltip, Legend);

interface DashboardStats {
  totalRecent: number;
  totalConfirmed: number;
  totalPending: number;
  totalCancelled: number;
  recentReservations: { id: number; customerName: string; date: string; status: string }[];
}

function useDashboardStats() {
  const [stats, setStats] = useState<DashboardStats | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Simular una llamada a la API
    setTimeout(() => {
      setStats({
        totalRecent: 15,
        totalConfirmed: 10,
        totalPending: 3,
        totalCancelled: 2,
        recentReservations: [
          { id: 1, customerName: 'Juan Pérez', date: '2025-08-01', status: 'Confirmado' },
          { id: 2, customerName: 'Ana Gómez', date: '2025-08-02', status: 'Pendiente' },
          { id: 3, customerName: 'Carlos Ruiz', date: '2025-08-03', status: 'Confirmado' },
        ],
      });
      setLoading(false);
    }, 1000);
  }, []);

  return { stats, loading };
}

// Componente para las tarjetas de estadísticas
const StatCard = ({ title, value, icon, bgColor, description }) => (
  <div className={`p-6 rounded-lg shadow-lg text-white ${bgColor} transform transition-transform duration-300 hover:scale-105`}>
    <div className="flex items-center justify-between mb-2">
      <h2 className="text-sm font-light uppercase">{title}</h2>
      {icon}
    </div>
    <p className="text-4xl font-bold">{value}</p>
    {description && <p className="text-xs mt-2 opacity-80">{description}</p>}
  </div>
);

// Componente principal de la página
export default function Page() {
  const { stats, loading } = useDashboardStats();

  if (loading) {
    return (
      <div className="flex flex-1 flex-col gap-4 p-8">
        <p className="text-center text-lg">Cargando datos del dashboard...</p>
      </div>
    );
  }

  // Datos para el gráfico de torta
  const pieData = {
    labels: ['Confirmadas', 'Pendientes', 'Canceladas'],
    datasets: [
      {
        data: [stats?.totalConfirmed, stats?.totalPending, stats?.totalCancelled],
        backgroundColor: ['#10B981', '#F59E0B', '#EF4444'], // green, yellow, red
        borderColor: ['#ffffff', '#ffffff', '#ffffff'],
        borderWidth: 1,
      },
    ],
  };

  const pieOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top' as const,
      },
      title: {
        display: true,
        text: 'Distribución de Reservas',
      },
    },
  };

  return (
    <div className="flex flex-1 flex-col gap-8 p-8 bg-gray-100 min-h-screen">
      <h1 className="text-3xl font-extrabold text-gray-800 border-b-2 border-gray-300 pb-2">
        Dashboard de Reservas
      </h1>

      {/* Sección de Tarjetas de Estadísticas */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        <StatCard
          title="Reservas Recientes"
          value={stats?.totalRecent}
          icon={<FaCalendarAlt size={24} />}
          bgColor="bg-gradient-to-br from-blue-500 to-blue-700"
          description="Nuevas reservas en la última semana."
        />
        <StatCard
          title="Reservas Confirmadas"
          value={stats?.totalConfirmed}
          icon={<FaCheckCircle size={24} />}
          bgColor="bg-gradient-to-br from-green-500 to-green-700"
          description="Total de reservas confirmadas."
        />
        <StatCard
          title="Reservas Pendientes"
          value={stats?.totalPending}
          icon={<FaExclamationCircle size={24} />}
          bgColor="bg-gradient-to-br from-yellow-400 to-yellow-600"
          description="Reservas a la espera de confirmación."
        />
        <StatCard
          title="Reservas Canceladas"
          value={stats?.totalCancelled}
          icon={<FaTimesCircle size={24} />}
          bgColor="bg-gradient-to-br from-red-500 to-red-700"
          description="Reservas que fueron canceladas."
        />
      </div>

      {/* Sección de Gráficos y Últimas Reservas */}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-6">
        {/* Contenedor del Gráfico */}
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold mb-4 text-gray-700">Distribución de Reservas</h3>
          <div className="h-64">
             <Pie data={pieData} options={pieOptions} />
          </div>
        </div>

        {/* Contenedor de Últimas Reservas */}
        <div className="bg-white p-6 rounded-lg shadow-lg">
          <h3 className="text-xl font-semibold mb-4 text-gray-700">Últimas Reservas</h3>
          <ul className="space-y-4">
            {stats?.recentReservations.map((r) => (
              <li
                key={r.id}
                className="flex items-center p-4 bg-gray-50 rounded-lg border border-gray-200 transition-shadow duration-300 hover:shadow-md"
              >
                <div className="flex-1">
                  <p className="font-medium text-gray-800">{r.customerName}</p>
                  <p className="text-sm text-gray-500">
                    {new Date(r.date).toLocaleDateString('es-ES', {
                      day: '2-digit',
                      month: 'long',
                      year: 'numeric',
                    })}
                  </p>
                </div>
                <span
                  className={`text-sm font-semibold px-3 py-1 rounded-full capitalize ${
                    r.status === 'confirmed'
                      ? 'bg-green-100 text-green-800'
                      : r.status === 'pending'
                      ? 'bg-yellow-100 text-yellow-800'
                      : 'bg-red-100 text-red-800'
                  }`}
                >
                  {r.status}
                </span>
              </li>
            ))}
          </ul>
        </div>
      </div>
    </div>
  );
}