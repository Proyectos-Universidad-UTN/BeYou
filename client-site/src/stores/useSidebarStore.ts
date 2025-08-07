import { create } from "zustand";

interface SidebarStore {
  isExpanded: boolean;
  setIsExpanded: (value: boolean) => void;
}

export const useSidebarStore = create<SidebarStore>((set) => ({
  isExpanded: false,
  setIsExpanded: (value) => set({ isExpanded: value }),
}));
