"use client";

import { List } from "@mui/material";
import { MenuItem } from "./Types";
import { renderMenuItem } from "./RenderMenuItem";
import { useSidebarStore } from "@/stores/useSidebarStore"; 

interface DesktopSidebarProps {
  menuItems: MenuItem[];
}

export default function DesktopSidebar({ menuItems }: DesktopSidebarProps) {
  const { isExpanded, setIsExpanded } = useSidebarStore(); 

  return (
    <div
      onMouseEnter={() => setIsExpanded(true)}
      onMouseLeave={() => setIsExpanded(false)}
      className={`fixed top-0 left-0 h-full z-50 transition-all duration-300 ${
        isExpanded ? "w-64" : "w-16"
      } bg-pink-200`}
    >
      <List>{menuItems.map((item) => renderMenuItem(item, false))}</List>
    </div>
  );
}
