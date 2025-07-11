"use client";

import MobileSidebar from "./MobileSidebar";
import menuItems from "@/navigation/Routes";
import DesktopSidebar from "./DesktopSidebar";
import { useIsMobile } from "@/hooks/UseIsMobile";

interface SidebarProps {
  isOpen?: boolean;
  onClose?: () => void;
}

export default function Sidebar({ isOpen, onClose }: SidebarProps) {
  const isMobile = useIsMobile();

  return isMobile ? (
    <MobileSidebar isOpen={isOpen} onClose={onClose} menuItems={menuItems} />
  ) : (
    <DesktopSidebar menuItems={menuItems} />
  );
}
