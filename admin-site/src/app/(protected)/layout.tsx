//import WithLayout from "@/components/Layout/WithLayout";

//export default function ProtectedLayout({ children }: { children: React.ReactNode }) {
//  return <WithLayout>{children}</WithLayout>;
//}
"use client";

import React, { useState } from "react"; 
import Sidebar from "@/components/Sidebar/Sidebar";
import Header from "@/components/Header/Header";

export default function WithLayout({ children }: { children: React.ReactNode }) {
  const [sidebarOpen, setSidebarOpen] = useState(false); 

  const handleSidebarToggle = () => {
    setSidebarOpen(!sidebarOpen);
  };

  return (
    <div className="flex h-screen overflow-hidden">
      {}
      {}
      <div className="hidden md:block w-[260px] bg-pink-100 flex-shrink-0">
        <Sidebar />
      </div>

      {}
      {}
      <div className="md:hidden">
        <Sidebar isOpen={sidebarOpen} onClose={handleSidebarToggle} />
      </div>

      {}
      <div className="flex flex-col flex-1 overflow-auto">
        {}
        <div className="sticky top-0 z-50 bg-[#f9fbfd]">
          <Header onMenuClick={handleSidebarToggle} /> {}
        </div>

        {}
        <main className="flex-1 p-6 bg-gray-50">{children}</main>
      </div>
    </div>
  );
}
