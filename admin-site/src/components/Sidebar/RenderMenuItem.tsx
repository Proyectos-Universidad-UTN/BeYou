/* eslint-disable react-hooks/rules-of-hooks */
"use client";

import React, { useState } from "react";
import Link from "next/link";
import {
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Collapse,
} from "@mui/material";
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { usePathname } from "next/navigation";
import { MenuItem } from "./Types";
import { useSidebarStore } from "@/stores/useSidebarStore";
import { startsWith } from "lodash";

interface RenderMenuItemProps {
  item: MenuItem;
  isSmallScreen: boolean;
  onClose?: () => void;
  isCollapsed?: boolean;
}

export function renderMenuItem(
  item: MenuItem,
  isSmallScreen: boolean,
  onClose?: () => void
) {
  const pathname = usePathname();
  const { isExpanded } = useSidebarStore();
  const [expandedMenus, setExpandedMenus] = useState<{
    [key: string]: boolean;
  }>({});

  const handleToggle = (label: string) => {
    setExpandedMenus((prev) => ({ ...prev, [label]: !prev[label] }));
  };

  const isActive = startsWith(pathname, item.path);
  const isExpandedItem = expandedMenus[item.label];
  const icon = item.icon ? <item.icon className="w-5 h-5" /> : null;

  // Elemento con hijos (submenú)
  if (item.children) {
    return (
      <React.Fragment key={item.label}>
        <ListItem
          onClick={() => handleToggle(item.label)}
          className={`cursor-pointer px-4 py-2 rounded-xl transition-colors duration-300 mb-1 flex items-center justify-between ${
            isActive || isExpandedItem
              ? "bg-yellow-300/90 text-black shadow-md"
              : "text-gray-800 hover:bg-yellow-100/80 hover:text-black"
          }`}
        >
          <ListItemIcon
            className={`${isActive ? "text-yellow-700" : "text-gray-800"}`}
          >
            {icon}
          </ListItemIcon>

          {(isExpanded || isSmallScreen) && (
            <ListItemText primary={item.label} />
          )}

          {(isExpanded || isSmallScreen) &&
            (isExpandedItem ? (
              <ExpandLess className="text-yellow-700" />
            ) : (
              <ExpandMore className="text-yellow-700" />
            ))}
        </ListItem>

        <Collapse
          in={isExpandedItem && (isExpanded || isSmallScreen)}
          timeout="auto"
          unmountOnExit
        >
          <List component="div" disablePadding>
            {item.children.map((child) => {
              const childActive = startsWith(pathname, child.path);
              const childIcon = child.icon ? (
                <child.icon className="w-5 h-5" />
              ) : null;

              return (
                <Link href={child.path!} key={child.label} passHref>
                  <ListItem
                    {...(isSmallScreen && onClose ? { onClick: onClose } : {})}
                    className={`text-sm rounded-xl px-4 py-2 mb-2 flex items-center transition-colors duration-300 ${
                      childActive
                        ? "bg-yellow-300 text-black shadow-sm"
                        : "text-black hover:bg-yellow-100 hover:text-black"
                    }`}
                  >
                    <ListItemIcon
                      className={`min-w-[36px] ${
                        childActive ? "text-yellow-700" : "text-black"
                      }`}
                    >
                      {childIcon}
                    </ListItemIcon>

                    {(isExpanded || isSmallScreen) && (
                      <ListItemText primary={child.label} />
                    )}
                  </ListItem>
                </Link>
              );
            })}
          </List>
        </Collapse>
      </React.Fragment>
    );
  }

  // Elemento sin hijos
  return (
    <Link href={item.path!} key={item.label} passHref>
      <ListItem
        {...(isSmallScreen && onClose ? { onClick: onClose } : {})}
        className={`cursor-pointer px-4 py-2 rounded-xl mb-1 flex items-center transition-colors duration-300 ${
          isActive
            ? "bg-yellow-300/90 text-black shadow-md"
            : "text-gray-800 hover:bg-yellow-100/80 hover:text-black"
        }`}
      >
        <ListItemIcon
          className={`${isActive ? "text-yellow-700" : "text-gray-800"}`}
        >
          {icon}
        </ListItemIcon>

        {(isExpanded || isSmallScreen) && (
          <ListItemText primary={item.label} />
        )}
      </ListItem>
    </Link>
  );
}
