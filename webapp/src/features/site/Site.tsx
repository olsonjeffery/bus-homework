
import React, { useState } from 'react';

import { AppBar, Box, Tab, Tabs } from '@mui/material';
import Typography from '@mui/material/Typography';

import { useAppSelector, useAppDispatch } from '../../app/hooks';
import {
  setActiveTab,
  selectActiveTab,
} from './siteSlice';
import { store } from '../../app/store';

import { theme } from '../../theme-options';

import { Stop } from '../stop/Stop';
import { selectLastChecked } from '../stop/stopSlice';

import { TabPanel, a11yProps } from './tab-panel';


const handleChange = (event: React.SyntheticEvent, newValue: number) => {
  store.dispatch(setActiveTab(newValue));
};

export function Site() {
  const activeTabValue: number = useAppSelector(selectActiveTab);
  const lastChecked: string = useAppSelector(selectLastChecked);

  return (
    <div className="site-container">
      <AppBar position="static">
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Bus Homework
          </Typography>
        <Tabs
          value={activeTabValue}
          onChange={handleChange}
          indicatorColor="secondary"
          textColor="inherit"
          variant="fullWidth"
          aria-label="full width tabs"
        >
          <Tab label="Stops 1 & 2" {...a11yProps(0)} />
        </Tabs>
      </AppBar>
      <TabPanel value={activeTabValue} index={0}>
        <h6>last checked {lastChecked}</h6>
      </TabPanel>
      <Stop></Stop>
    </div>
  );
}