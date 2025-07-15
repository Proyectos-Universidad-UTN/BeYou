"use client";

import { JSX, useState } from "react";
import DeleteIcon from "@mui/icons-material/Delete";
import {
  Box,
  Checkbox,
  IconButton,
  List,
  ListItem,
  ListItemText,
  Typography,
} from "@mui/material";

interface ListViewProps<T extends { id: number }> {
  title: string;
  data: T[];
  fieldForPrimaryText: keyof T | ((item: T) => string);
  onDelete: (ids: number[]) => void;
  enableDense?: boolean;
  enableSecondaryText?: boolean;
  fieldForSecondaryText?: keyof T | ((item: T) => string) | null;
}

const removeItemFromSelection = (prev: number[], id: number): number[] =>
  prev.filter((itemId) => itemId !== id);

export const ListViewWithDelete = <T extends { id: number }>({
  title,
  data,
  fieldForPrimaryText,
  onDelete,
  enableDense = true,
  enableSecondaryText = false,
  fieldForSecondaryText = null,
}: ListViewProps<T>): JSX.Element => {
  const [selectedItems, setSelectedItems] = useState<number[]>([]);
  const [selectAll, setSelectAll] = useState(false);

  const handleDeleteClick = () => {
    onDelete(selectedItems);
    setSelectedItems([]);
    setSelectAll(false);
  };

  const handleDeleteById = (id: number) => {
    onDelete([id]);
    setSelectedItems((prev) => removeItemFromSelection(prev, id));
  };

  const handleSelectAllChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const checked = event.target.checked;
    setSelectAll(checked);
    setSelectedItems(checked ? data.map((item) => item.id) : []);
  };

  const handleCheckboxChange =
    (id: number) => (event: React.ChangeEvent<HTMLInputElement>) => {
      const checked = event.target.checked;
      setSelectedItems((prev) =>
        checked ? [...prev, id] : removeItemFromSelection(prev, id)
      );
    };

  const getPrimaryText = (item: T): string =>
    typeof fieldForPrimaryText === "function"
      ? fieldForPrimaryText(item)
      : String(item[fieldForPrimaryText]);

  const getSecondaryText = (item: T): string | null => {
    if (enableSecondaryText && fieldForSecondaryText) {
      return typeof fieldForSecondaryText === "function"
        ? fieldForSecondaryText(item)
        : String(item[fieldForSecondaryText]);
    }
    return null;
  };

  return (
    <Box sx={{ flexGrow: 1, pb: 2, width: "100%" }}>
      <Box
        sx={{
          width: "100%",
          display: "flex",
          flexDirection: "column",
          gap: 2,
        }}
      >
        <Box
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "space-between",
            backgroundColor: "#5a7f7f",
            color: "white",
            padding: 2,
            borderRadius: "2px",
            minHeight: "60px",
          }}
        >
          <Checkbox
            checked={selectAll}
            onChange={handleSelectAllChange}
            sx={{ color: "white", p: 0 }}
            slotProps={{ input: { "aria-label": "select all" } }}
          />
          <Typography
            variant="h6"
            component="div"
            sx={{ flexGrow: 1, textAlign: "center" }}
          >
            {title}
          </Typography>
          <IconButton
            onClick={handleDeleteClick}
            edge="end"
            disabled={selectedItems.length === 0}
            sx={{ "&:hover": { color: "red" } }}
          >
            <DeleteIcon fontSize="large" />
          </IconButton>
        </Box>

        <List sx={{ py: 0 }} dense={enableDense}>
          {data.length === 0 ? (
            <ListItem
              sx={{
                py: "10px",
                borderBottom: "1px solid black",
                backgroundColor: "#e5e5e5",
                flexDirection: "column",
              }}
            >
              <Typography fontWeight="bold">Sin registros</Typography>
            </ListItem>
          ) : (
            data.map((item, index) => (
              <ListItem
                key={`${item.id}-${index}`}
                sx={{
                  px: 1,
                  py: "1rem",
                  borderBottom: "1px solid black",
                  backgroundColor: "#e5e5e5",
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                }}
                secondaryAction={
                  <IconButton
                    edge="end"
                    onClick={() => handleDeleteById(item.id)}
                  >
                    <DeleteIcon fontSize="large" />
                  </IconButton>
                }
              >
                <Checkbox
                  checked={selectedItems.includes(item.id)}
                  onChange={handleCheckboxChange(item.id)}
                  color="primary"
                  sx={{ marginRight: 2 }}
                />
                <ListItemText
                  primary={getPrimaryText(item)}
                  secondary={getSecondaryText(item)}
                />
              </ListItem>
            ))
          )}
        </List>
      </Box>
    </Box>
  );
};
