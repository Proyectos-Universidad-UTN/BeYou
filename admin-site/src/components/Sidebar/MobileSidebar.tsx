"use client";

import { Drawer, List } from "@mui/material";
import { MenuItem } from "./Types";
import { renderMenuItem } from "./RenderMenuItem";

interface MobileSidebarProps {
  isOpen?: boolean;
  onClose?: () => void;
  menuItems: MenuItem[];
}

export default function MobileSidebar({
  isOpen,
  onClose,
  menuItems,
}: MobileSidebarProps) {
  const header = (
    <div className="font-bold text-xl text-center py-4 text-yellow-700 bg-pink-300 tracking-wide border-b" />
  );

  return (
    <Drawer
      variant="temporary"
      open={isOpen}
      onClose={onClose}
      className="md:hidden"
      slotProps={{
        paper: {
          className: "w-3/4", 
        },
      }}
    >
      <div className="bg-pink-200 h-full flex flex-col">
        {header}
        <List className="flex-1 overflow-y-auto">
          {menuItems.map((item) => renderMenuItem(item, true, onClose))}
        </List>
      </div>
    </Drawer>
  );
}
