import os
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
from scipy import signal
import glob
import os

#def create_folder(folder_name):
#    # Get the directory where the script is located
#    script_directory = os.path.dirname(os.path.abspath(__file__))
#    
#    # Define the full path of the new folder
#    folder_path = os.path.join(script_directory, folder_name)
#    
#    # Create the folder if it does not exist
#    if not os.path.exists(folder_path):
#        os.makedirs(folder_path)
#        print(f"Folder '{folder_name}' created successfully at {folder_path}")
#    else:
#        print(f"Folder '{folder_name}' already exists at {folder_path}")

       

# Example usage
#folder_name = "pngs"
#create_folder(folder_name)

def run_stft(folder_name, csv_file):
    
    #FOLDER
    # Get the directory where the script is located
    script_directory = os.path.dirname(os.path.abspath(__file__))
    
    # Define the full path of the new folder
    folder_path = os.path.join(script_directory, folder_name)
    
    # Create the folder if it does not exist
    if not os.path.exists(folder_path):
        os.makedirs(folder_path)
        print(f"Folder '{folder_name}' created successfully at {folder_path}")
    else:
        print(f"Folder '{folder_name}' already exists at {folder_path}")


    ##################

    #STFT
    script_directory = os.path.dirname(os.path.abspath(__file__))
    imgName = os.path.join(script_directory, folder_name) + "/" + os.path.splitext(os.path.basename(csv_file))[0] + ".png"

    data = pd.read_csv(csv_file, delimiter=';', skiprows=2, names=["Time (ms)", "Channel A (V)"])

    # Replace commas with dots and convert columns to float
    data["Time (ms)"] = data["Time (ms)"].str.replace(',', '.').astype(float)
    data["Channel A (V)"] = data["Channel A (V)"].str.replace(',', '.').astype(float)
    t = data["Time (ms)"]
    mySignal = data["Channel A (V)"]

    #centrowanie
    signal_centered = mySignal - np.mean(mySignal)

    mySignal = signal_centered
    mySignal =mySignal.iloc[50:350]

    n = len(mySignal)
    sample_rate = 1 / (t.diff().mean() / 1000)
    fft_freqs = np.fft.fftfreq(n, d=1/sample_rate)

    f, t, Zxx = signal.stft(mySignal, fs=sample_rate, nperseg=512)

    plt.figure(figsize=(8, 8))
    plt.axis('off')
    plt.pcolormesh(t, f, np.abs(Zxx), shading='auto')

    #plt.show()
    plt.savefig(imgName, bbox_inches='tight', pad_inches=0)



#def stft(csv_file):
#    
#    script_directory = os.path.dirname(os.path.abspath(__file__))
#    imgName = os.path.join(script_directory, folder_name)
#
#    data = pd.read_csv(csv_file, delimiter=';', skiprows=2, names=["Time (ms)", "Channel A (V)"])
#
#    # Replace commas with dots and convert columns to float
#    data["Time (ms)"] = data["Time (ms)"].str.replace(',', '.').astype(float)
#    data["Channel A (V)"] = data["Channel A (V)"].str.replace(',', '.').astype(float)
#    t = data["Time (ms)"]
#    mySignal = data["Channel A (V)"]
#
#    #centrowanie
#    signal_centered = mySignal - np.mean(mySignal)

#    mySignal = signal_centered

#    n = len(mySignal)
#    sample_rate = 1 / (t.diff().mean() / 1000)
#    fft_freqs = np.fft.fftfreq(n, d=1/sample_rate)
#
#   f, t, Zxx = signal.stft(mySignal, fs=sample_rate, nperseg=512)

#    plt.figure(figsize=(8, 8))
#    plt.axis('off')
#    plt.pcolormesh(t, f, np.abs(Zxx), shading='auto')

    #plt.show()
#    plt.savefig(imgName, bbox_inches='tight', pad_inches=0)
