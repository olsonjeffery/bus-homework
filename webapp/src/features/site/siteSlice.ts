import { createAsyncThunk, createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Root } from 'react-dom/client';
import { RootState, AppThunk } from '../../app/store';

export interface SiteState {
  activeTab: number
}

const initialState: SiteState = {
  activeTab: 0
};

const siteSlice = createSlice({
  name: 'site',
  initialState,
  reducers: {
    setActiveTab: (state, action: PayloadAction<number>) => {
      state.activeTab = action.payload;
    }
  }
});

export const {setActiveTab} = siteSlice.actions;

export const selectActiveTab = (state: RootState) => state.site.activeTab;

export default siteSlice.reducer;