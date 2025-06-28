import { IconButton } from "@mui/material";
import MoreVertIcon from '@mui/icons-material/MoreVert';

interface OptionsBulletProps {
  handleMenuOpen: (event: React.MouseEvent<HTMLButtonElement>) => void;
}

export const OptionsBullet = ({ handleMenuOpen }: OptionsBulletProps) => {
  const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.stopPropagation();
    handleMenuOpen(event);
  };

  return (
    <IconButton onClick={handleClick} size="small">
      <MoreVertIcon />
    </IconButton>
  );
};
