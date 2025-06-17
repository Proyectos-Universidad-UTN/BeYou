"use client";

import { List } from "@mui/material";
import { MenuItem } from "./types";
import { renderMenuItem } from "./renderMenuItem";

interface DesktopSidebarProps {
  menuItems: MenuItem[];
}

export default function DesktopSidebar({ menuItems }: DesktopSidebarProps) {
  const header = (
    <div className="font-bold text-xl text-center py-4 text-yellow-700 bg-pink-300 tracking-wide border-b" />
  );

  return (
    <div className="fixed h-full shadow w-64 bg-pink-100">
      {header}
      <List>{menuItems.map((item) => renderMenuItem(item, false))}</List>
    </div>
  );
}
