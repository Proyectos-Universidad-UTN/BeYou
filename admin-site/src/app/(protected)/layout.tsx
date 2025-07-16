"use client";

import React, { useState } from "react";
import Sidebar from "@/components/Sidebar/Sidebar";
import Header from "@/components/Header/Header";
import { ProtectedRoute } from "@/navigation/ProtectedRoute";
import { Snackbar } from "@/components/Shared/Snackbar";
import { useSidebarStore } from "@/stores/useSidebarStore";

export default function WithLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const [sidebarOpen, setSidebarOpen] = useState(false);
  const { isExpanded } = useSidebarStore();

  const handleSidebarToggle = () => {
    setSidebarOpen(!sidebarOpen);
  };

  return (
    <ProtectedRoute>
      <div className="flex h-screen overflow-hidden">
        {/* Sidebar escritorio */}
        <div className="hidden md:block fixed left-0 top-0 h-full z-50">
          <Sidebar />
        </div>

        {/* Sidebar móvil */}
        <div className="md:hidden">
          <Sidebar isOpen={sidebarOpen} onClose={handleSidebarToggle} />
        </div>

        {/* Contenido principal */}
        <div
          className={`flex flex-col flex-1 transition-all duration-300 ${
            isExpanded ? "ml-64" : "ml-16"
          }`}
        >
          {/* Header */}
          <div className="sticky top-0 z-40 bg-[#f9fbfd] transition-all duration-300">
            <Header onMenuClick={handleSidebarToggle} />
          </div>

          {/* Snackbar global */}
          <Snackbar />

          {/* Contenido de la página */}
          <main className="flex-1 p-6 bg-gray-50 overflow-auto">
            {children}
          </main>
        </div>
      </div>
    </ProtectedRoute>
  );
}
