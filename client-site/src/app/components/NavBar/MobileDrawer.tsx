import {
  Drawer,
  IconButton,
  List,
  ListItemButton,
  ListItemText,
  Button,
} from "@mui/material";
import { Menu, X } from "lucide-react";
import { useState } from "react";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { ROUTES } from "@/navigation/Routes";

const menuItems = [
  { text: "Inicio", href: "/" },
  { text: "Servicios", href: "#servicios" },
  { text: "Productos", href: "#productos" },
];

export default function MobileDrawer() {
  const [open, setOpen] = useState(false);
  const router = useRouter();

  const toggleDrawer = () => setOpen(!open);

  return (
    <>
      <IconButton onClick={toggleDrawer}>
        <Menu />
      </IconButton>

      <Drawer anchor="right" open={open} onClose={toggleDrawer}>
        <div className="w-64 p-4">
          <div className="flex justify-end">
            <IconButton onClick={toggleDrawer}>
              <X />
            </IconButton>
          </div>

          <List>
            {menuItems.map((item) => (
              <Link href={item.href} key={item.text} passHref legacyBehavior>
                <ListItemButton
                  component="a"
                  onClick={toggleDrawer}
                  sx={{ textAlign: "left" }}
                >
                  <ListItemText primary={item.text} />
                </ListItemButton>
              </Link>
            ))}

            <ListItemButton sx={{ justifyContent: "center", mt: 2 }}>
              <Button
                fullWidth
                variant="contained"
                onClick={() => {
                  router.push(ROUTES.RESERVATION);
                  toggleDrawer();
                }}
                sx={{
                  bgcolor: "#7B68EE",
                  "&:hover": { bgcolor: "#6a5ad6" },
                }}
              >
                Reservar
              </Button>
            </ListItemButton>
          </List>
        </div>
      </Drawer>
    </>
  );
}
